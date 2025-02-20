using SpendingAnalyser.DTOs;

namespace SpendingAnalyser.Services;

public interface IStarlingService
{
    public Task<string> GetAccountUri(string token);

    public Task<IEnumerable<StatementDTO>> GetStatement(string token, string accountUid, string year, string month);
}
