using ManyEntitiesSender.BLL.Settings;
using ManyEntitiesSender.DAL.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using UnitedSystems.CommonLibrary.ManyEntitiesSender;
using UnitedSystems.CommonLibrary.ManyEntitiesSender.Interfaces;

namespace ManyEntitiesSender.BLL.Services.Abstractions
{
    public abstract class AbsObjectGenerator(IServiceScopeFactory scopeFactory, IOptions<PackageSettings> options, ILogger<AbsObjectGenerator> logger) : IObjectGenerator
    {
        public async Task EnsurePartsCount()
        {
            logger.LogDebug("Begin ensuring sets of entities");
            logger.LogTrace("Begin ensuring bodies");
            var bodyTask = Ensure<Body>();
            logger.LogTrace("Begin ensuring hands");
            var handTask = Ensure<Hand>();
            logger.LogTrace("Begin ensuring legs");
            var legTask = Ensure<Leg>();
            logger.LogDebug("Ensuring complete");

            var tasks = new List<Task> { bodyTask, handTask, legTask };
            while (tasks.Count > 0) {
                Task finishedTask = await Task.WhenAny(tasks);
                if (finishedTask == bodyTask) {
                    Console.WriteLine("Body are ensured");
                }
                else if (finishedTask == handTask) {
                    Console.WriteLine("Hands are ensured");
                }
                else if (finishedTask == legTask) {
                    Console.WriteLine("Legs are ensured");
                }
                await finishedTask;
                tasks.Remove(finishedTask);
            }

        }

        private async Task Ensure<TEntity>() where TEntity : class, IEntity, new()
        {
            using IServiceScope scope = scopeFactory.CreateScope();
            IPackageContext context = scope.ServiceProvider.GetRequiredService<IPackageContext>();

            TEntity checkEntity = new();
            if(!(checkEntity is Body || checkEntity is Hand || checkEntity is Leg))
                throw new NotImplementedException("Such entity type can't be created");

            int quantity = options.Value.PackageTotal;
            int count = await context.DbSet<TEntity>().CountAsync();
            int required = quantity - count;
            int package = options.Value.PackageCount;
            int iterations = required/package;

            if (required > 0) {
                foreach (int iteration in Enumerable.Range(1, iterations)) {
                    List<TEntity> entities = [];

                    foreach (int i in Enumerable.Range(0, package)) {
                        int testNo = i * iteration;
#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                        if(checkEntity is Body)
                        {
                            entities.Add(CreateBody(testNo) as TEntity);
                        }
                        else if(checkEntity is Hand)
                        {
                            entities.Add(CreateHand(testNo) as TEntity);
                        }
                        else if(checkEntity is  Leg)
                        {
                            entities.Add(CreateLeg(testNo) as TEntity);
                        }
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                    }
                    await context.DbSet<TEntity>().AddRangeAsync(entities);
                    await context.SaveChangesAsync();
                }
            }
        }

        protected abstract Body CreateBody(int testNo);
        protected abstract Hand CreateHand(int testNo);
        protected abstract Leg CreateLeg(int testNo);
    }
}
