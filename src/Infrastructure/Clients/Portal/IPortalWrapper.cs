using Defender.Common.Clients.Portal;

namespace Defender.GeneralTestingService.Infrastructure.Clients.Portal;

public interface IPortalWrapper
{
    Task<Session> Login(string username, string password);
    Task<string> AuthCheck();
    Task<PortalWalletInfoDto> GetWalletInfo();
}
