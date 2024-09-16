using System.Collections.Concurrent;
using System.Text.Json;

using ManyEntitiesSender.Attributes;
using ManyEntitiesSender.Models;
using ManyEntitiesSender.RAL.Abstractions;

using UnitedSystems.CommonLibrary.ManyEntitiesSender;
using UnitedSystems.CommonLibrary.ManyEntitiesSender.IntegrationEvents;
using UnitedSystems.EventBus.Interfaces;

namespace ManyEntitiesSender.Middleware
{
    public class CachingMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        // Мьютексы, мониторы, lock - они все требуют работы в синхронном контексте - т.е между wait и release не должно быть await
        //private static ConcurrentDictionary<string, Mutex> monitors = new();

        // А SemaphoreSlim вообще нормально
        private readonly static ConcurrentDictionary<string, SemaphoreSlim> semaphores = new();

        // Подсчет количества обработанных элементов для отправки в MDM
        private int _entityProcessedCount = 0;
        /// <summary>
        /// Если возвращает 200 - это контроллер создал, если 201 - то создал Redis
        /// </summary>
        public async Task InvokeAsync(HttpContext httpContext, IRedisProvider redis, IEventBus eventBus)
        {
            Endpoint? endpoint = httpContext.GetEndpoint();
            if(endpoint is not null) {
                if (endpoint.Metadata.Where(meta => meta is CacheableAttribute).Any())
                {
                    string? table = (string?)httpContext.Items["table"] ?? throw new NotFoundMiddlewareException();
                    string? filter = (string?)httpContext.Items["filter"];
                    
                    bool redisReturnedValue = await CheckRedis(httpContext, redis, table, filter);
                    if (redisReturnedValue)
                    {
                        return;
                    }
                    try
                    {
                        //monitors.GetOrAdd(table, new Mutex()).WaitOne(); // здесь происходит магия 2check
                        semaphores.GetOrAdd(table, new SemaphoreSlim(1,1)).Wait();
                        bool redisReturnedValueSecond = await CheckRedis(httpContext, redis, table, filter);
                        if (redisReturnedValueSecond) {
                            // httpContext.Response.StatusCode = 201;
                        }
                        else {
                            string responseBody;
                            // супер трюк
                            Stream originalBody = httpContext.Response.Body;
                            using (var memStream = new MemoryStream()) {
                                httpContext.Response.Body = memStream;

                                await _next(httpContext);

                                memStream.Position = 0;
                                responseBody = new StreamReader(memStream).ReadToEnd();

                                memStream.Position = 0;
                                await memStream.CopyToAsync(originalBody);
                                httpContext.Response.Body = originalBody;
                            }
                            await CacheResponseBody(redis, table, responseBody);
                        }
                        await eventBus.PublishAsync(new MESReturnedObjectsEvent() {
                            StatusCode = httpContext.Response.StatusCode,
                            Count = _entityProcessedCount
                        });
                    }
                    finally
                    {
                        //monitors.GetOrAdd(table, new Mutex()).ReleaseMutex();
                        semaphores.TryGetValue(table, out SemaphoreSlim? semaphore);
                        semaphore?.Release();
                    }
                }
                else {
                    // если endpoint без нужного атрибута, просто его пропускаем
                    await _next(httpContext);
                }
            }
        }

        private async Task CacheResponseBody(IRedisProvider redis, string? table, string responseBody)
        {
            if (table == "body")
            {
                Body[] bodies = JsonSerializer.Deserialize<Body[]?>(responseBody) ?? throw new ArgumentNullException(nameof(responseBody));
                _entityProcessedCount = bodies.Length;
                await redis.AppendListAsync(bodies);
            }
            else if (table == "hand")
            {
                Hand[] hands = JsonSerializer.Deserialize<Hand[]?>(responseBody) ?? throw new ArgumentNullException(nameof(responseBody));
                _entityProcessedCount = hands.Length;
                await redis.AppendListAsync(hands);
            }
            else if (table == "leg")
            {
                Leg[] legs = JsonSerializer.Deserialize<Leg[]?>(responseBody) ?? throw new ArgumentNullException(nameof(responseBody));
                _entityProcessedCount = legs.Length;
                await redis.AppendListAsync(legs);
            }
        }

        /// <summary>
        /// Кушает данные из стрима иии.. соединяет их в один пакет. Иначе логика потребуется очень сложная
        /// </summary>
        /// <returns></returns>
        private async Task<bool> CheckRedis(HttpContext httpContext, IRedisProvider redis, string requestedTable, string? filterValue)
        {
            bool redisReturnedValue = false;
            if (requestedTable.Equals("body", StringComparison.CurrentCultureIgnoreCase))
            {
                List<Body> values = [];
                await foreach (var package in redis.TryGetAsync<Body>(filterValue))
                {
                    if (package is null)
                        break;
                    if (package.Count == 0)
                        break;

                    values.AddRange(package);

                    redisReturnedValue = true;
                }
                if (values.Count > 0) {
                    _entityProcessedCount = values.Count;
                    httpContext.Response.StatusCode = 201;
                    await httpContext.Response.WriteAsJsonAsync(values);
                }
            }
            if (requestedTable.Equals("hand", StringComparison.CurrentCultureIgnoreCase))
            {
                List<Hand> values = [];
                await foreach (var package in redis.TryGetAsync<Hand>(filterValue))
                {
                    if (package is null)
                        break;
                    if (package.Count == 0)
                        break;

                    values.AddRange(package);

                    redisReturnedValue = true;
                }
                if (values.Count > 0) {
                    _entityProcessedCount = values.Count;
                    httpContext.Response.StatusCode = 201;
                    await httpContext.Response.WriteAsJsonAsync(values);
                }
                    
            }
            if (requestedTable.Equals("leg", StringComparison.CurrentCultureIgnoreCase))
            {
                List<Leg> values = [];
                await foreach (var package in redis.TryGetAsync<Leg>(filterValue))
                {
                    if (package is null)
                        break;
                    if (package.Count == 0)
                        break;

                    values.AddRange(package);

                    redisReturnedValue = true;
                }
                if (values.Count > 0) {
                    _entityProcessedCount = values.Count;
                    httpContext.Response.StatusCode = 201;
                    await httpContext.Response.WriteAsJsonAsync(values);
                }
            }

            return redisReturnedValue;
        }
    }

    public static class CachingMiddlewareExtensions
    {
        /// <summary>
        /// Должно идти после UseMyCachingValidation
        /// </summary>
        public static IApplicationBuilder UseMyCaching(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CachingMiddleware>();
        }
    }
}
