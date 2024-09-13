namespace ManyEntitiesSender.Models
{
    public class NotFoundMiddlewareException(string message = "UseMyCaching must be after UseMyCachingValidation") : Exception(message)
    {
    }
}
