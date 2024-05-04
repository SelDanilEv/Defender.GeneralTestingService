using AutoMapper;
using Defender.Common.Clients.Portal;
using Defender.Common.Clients.Wallet;
using Defender.Common.Interfaces;
using Defender.Common.Wrapper.Internal;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using LoginWithPasswordCommand = Defender.Common.Clients.Portal.LoginWithPasswordCommand;
using StartTransferTransactionCommand = Defender.Common.Clients.Portal.StartTransferTransactionCommand;
using Currency = Defender.Common.Clients.Portal.Currency;

namespace Defender.GeneralTestingService.Infrastructure.Clients.Portal;

public class PortalWrapper : BaseInternalSwaggerWrapper, IPortalWrapper
{
    private readonly IMapper _mapper;
    private readonly IPortalApiClient _portalApiClient;

    public PortalWrapper(
        IAuthenticationHeaderAccessor authenticationHeaderAccessor,
        IPortalApiClient portalApiClient,
        IMapper mapper) : base(
            portalApiClient,
            authenticationHeaderAccessor)
    {
        _portalApiClient = portalApiClient;
        _mapper = mapper;
    }

    public async Task<Session> LoginAsync(string email, string password)
    {
        return await ExecuteSafelyAsync(async () =>
        {
            var command = new LoginWithPasswordCommand()
            {
                Login = email,
                Password = password
            };

            return await _portalApiClient.LoginAsync(command);
        }, AuthorizationType.User);
    }

    public async Task<string> AuthCheckAsync()
    {
        return await ExecuteSafelyAsync(async () =>
        {
            return (await _portalApiClient.CheckAsync()).HighestRole;
        }, AuthorizationType.User);
    }

    public async Task<PortalWalletInfoDto> GetWalletInfoAsync()
    {
        return await ExecuteSafelyAsync(async () =>
        {
            return await _portalApiClient.InfoAsync(new GetWalletInfoQuery());
        }, AuthorizationType.User);
    }

    public async Task<PortalTransactionDto> TransferMoneyAsync(
        int walletNumber,
        int amount,
        Currency currency)
    {
        return await ExecuteSafelyAsync(async () =>
        {
            var command = new StartTransferTransactionCommand()
            {
                WalletNumber = walletNumber,
                Amount = amount,
                Currency = currency
            };

            return await _portalApiClient.TransferAsync(command);
        }, AuthorizationType.User);
    }

    public async Task<PortalTransactionDto> GetLatestTransactionAsync()
    {
        return await ExecuteSafelyAsync(async () =>
        {
            var transactions = await _portalApiClient.HistoryAsync(0, 1);

            if (transactions == null
            || transactions.Items == null 
            || !transactions.Items.Any())
            {
                throw new Exception("No transaction");
            }

            return transactions.Items.First();
        }, AuthorizationType.User);
    }
}
