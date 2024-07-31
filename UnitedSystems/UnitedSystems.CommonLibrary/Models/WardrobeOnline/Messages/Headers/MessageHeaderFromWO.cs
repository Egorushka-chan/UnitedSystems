namespace UnitedSystems.CommonLibrary.Models.WardrobeOnline.Messages.Headers
{
    public enum MessageHeaderFromWO
    {
        NotSpecified = 0,
        AppStarting = 1,
        AppClose = 2,
        PostRequestInfo = 3,
        PutRequestInfo = 4,
        GetRequestInfo = 5,
        DeleteRequestInfo = 6,
        ErrorResponseInfo = 7,
        PagedGetInfo = 8
    }
}
