namespace MasterDominaSystem.BLL.Services.Abstractions
{
    public interface IProcedureBaker
    {
        Task<string> AssertBaked(string reportKey);
        bool IsBaked(string reportKey);
        Task<string> BakeDefaultAsync(string? reportKey = default);
    }
}
