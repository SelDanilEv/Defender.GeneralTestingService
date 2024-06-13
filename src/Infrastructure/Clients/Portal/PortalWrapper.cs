using Defender.Common.Clients.Portal;
using Defender.Common.Helpers;
using Defender.Common.Interfaces;
using Defender.Common.Wrapper.Internal;
using Defender.GeneralTestingService.Application.Clients.Portal;
using Defender.GeneralTestingService.Domain.Enums;
using LoginWithPasswordCommand = Defender.Common.Clients.Portal.LoginWithPasswordCommand;
using StartTransferTransactionCommand = Defender.Common.Clients.Portal.StartTransferTransactionCommand;

namespace Defender.GeneralTestingService.Infrastructure.Clients.Portal;

public class PortalWrapper(
    IAuthenticationHeaderAccessor authenticationHeaderAccessor,
    IPortalApiClient portalApiClient) : BaseInternalSwaggerWrapper(
        portalApiClient,
        authenticationHeaderAccessor), IPortalWrapper
{
    public async Task<SessionDto> LoginAsync(string email, string password)
    {
        return await ExecuteUnsafelyAsync(async () =>
        {
            var command = new LoginWithPasswordCommand()
            {
                Login = email,
                Password = password
            };

            return await portalApiClient.Login2Async(command);
        }, AuthorizationType.User);
    }

    public async Task<string> AuthCheckAsync()
    {
        return await ExecuteUnsafelyAsync(async () =>
        {
            return (await portalApiClient.CheckAsync()).HighestRole.ToString();
        }, AuthorizationType.User);
    }

    public async Task<PortalWalletInfoDto> GetWalletInfoAsync()
    {
        return await ExecuteUnsafelyAsync(async () =>
        {
            return await portalApiClient.InfoAsync(new GetWalletInfoQuery());
        }, AuthorizationType.User);
    }

    public async Task<PortalTransactionDto> TransferMoneyAsync(
        int walletNumber,
        int amount,
        Currency currency)
    {
        return await ExecuteUnsafelyAsync(async () =>
        {
            var command = new StartTransferTransactionCommand()
            {
                WalletNumber = walletNumber,
                Amount = amount,
                Currency = MappingHelper.MapEnum
                    (currency, StartTransferTransactionCommandCurrency.Unknown)
            };

            return await portalApiClient.TransferAsync(command);
        }, AuthorizationType.User);
    }

    public async Task<PortalTransactionDto> RechargeMoneyAsync(
        int walletNumber,
        int amount,
        Currency currency)
    {
        return await ExecuteUnsafelyAsync(async () =>
        {
            var command = new StartRechargeTransactionCommand()
            {
                WalletNumber = walletNumber,
                Amount = amount,
                Currency = MappingHelper.MapEnum
                    (currency, StartRechargeTransactionCommandCurrency.Unknown)
            };

            return await portalApiClient.RechargeAsync(command);
        }, AuthorizationType.Service);
    }

    public async Task<List<PortalTransactionDto>> GetLatest50TransactionsAsync()
    {
        return await ExecuteUnsafelyAsync(async () =>
        {
            var transactions = await portalApiClient.HistoryAsync(null, 0, 50);

            if (transactions == null
            || transactions.Items == null
            || !transactions.Items.Any())
            {
                throw new Exception("No transaction");
            }

            return transactions.Items.ToList();
        }, AuthorizationType.User);
    }
}
