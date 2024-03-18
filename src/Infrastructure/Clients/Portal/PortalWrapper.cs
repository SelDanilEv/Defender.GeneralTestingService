using AutoMapper;
using Defender.Common.Clients.Portal;
using Defender.Common.Clients.Wallet;
using Defender.Common.Interfaces;
using Defender.Common.Wrapper.Internal;
using LoginWithPasswordCommand = Defender.Common.Clients.Portal.LoginWithPasswordCommand;

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

    public async Task<Session> Login(string email, string password)
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

    public async Task<string> AuthCheck()
    {
        return await ExecuteSafelyAsync(async () =>
        {
            return (await _portalApiClient.CheckAsync()).HighestRole;
        }, AuthorizationType.User);
    }

    public async Task<PortalWalletInfoDto> GetWalletInfo()
    {
        return await ExecuteSafelyAsync(async () =>
        {
            return await _portalApiClient.InfoAsync(new GetWalletInfoQuery());
        }, AuthorizationType.User);
    }
    
}
