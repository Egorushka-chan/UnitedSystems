namespace MasterDominaSystem.BLL.Services.Abstractions
{
    public interface IProcedureBaker
    {
        Task AssertBaked(string reportKey);
        bool IsBaked(string reportKey);
        Task BakeDefaultAsync(string? reportKey = default);
    }
}
