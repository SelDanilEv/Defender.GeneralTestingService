using Defender.GeneralTestingService.Application.Models;
using Defender.GeneralTestingService.Infrastructure.Clients.Portal;
using Defender.GeneralTestingService.Infrastructure.Steps.Interfaces;

namespace Defender.GeneralTestingService.Infrastructure.Steps;

public class VerifyWalletStep : IStep
{
    private const string StepName = "Wallet Verification";

    private readonly IPortalWrapper _portalWrapper;

    public VerifyWalletStep(IPortalWrapper portalWrapper)
    {
        _portalWrapper = portalWrapper;
    }

    public async Task<TestInstance> StartAsync(TestInstance instance)
    {
        try
        {
            var walletInfo = await _portalWrapper.GetWalletInfoAsync();

            if (walletInfo == null)
            {
                throw new Exception("Wallet info is not exist");
            }

            instance.AddSuccessLog(StepName);
        }
        catch (Exception ex)
        {
            instance.AddFailedLog(StepName, ex.Message);
        }

        return instance;
    }
}
