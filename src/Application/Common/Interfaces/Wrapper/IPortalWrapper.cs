using Defender.Common.Clients.Portal;
using Defender.GeneralTestingService.Domain.Enums;

namespace Defender.GeneralTestingService.Application.Clients.Portal;

public interface IPortalWrapper
{
    Task<SessionDto> LoginAsync(string username, string password);
    Task<string> AuthCheckAsync();
    Task<PortalWalletInfoDto> GetWalletInfoAsync();
    Task<PortalTransactionDto> TransferMoneyAsync(
        int walletNumber,
        int amount,
        Currency currency);
    Task<PortalTransactionDto> RechargeMoneyAsync(
       int walletNumber,
       int amount,
       Currency currency);
    Task<List<PortalTransactionDto>> GetLatest50TransactionsAsync();
}
