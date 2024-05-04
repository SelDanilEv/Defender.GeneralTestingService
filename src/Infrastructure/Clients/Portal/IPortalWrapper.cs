using Defender.Common.Clients.Portal;

namespace Defender.GeneralTestingService.Infrastructure.Clients.Portal;

public interface IPortalWrapper
{
    Task<Session> LoginAsync(string username, string password);
    Task<string> AuthCheckAsync();
    Task<PortalWalletInfoDto> GetWalletInfoAsync();
    Task<PortalTransactionDto> TransferMoneyAsync(
        int walletNumber,
        int amount,
        Currency currency);
    Task<PortalTransactionDto> GetLatestTransactionAsync();
}
