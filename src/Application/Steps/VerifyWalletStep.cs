using Defender.GeneralTestingService.Application.Models;
using Defender.GeneralTestingService.Application.Steps.Interfaces;
using Defender.GeneralTestingService.Application.Clients.Portal;

namespace Defender.GeneralTestingService.Application.Steps;

public class VerifyWalletStep(IPortalWrapper portalWrapper) : IStep
{
    private const string StepName = "Wallet Verification";

    public async Task<TestInstance> StartAsync(TestInstance instance)
    {
        try
        {
            var walletInfo =
                await portalWrapper.GetWalletInfoAsync()
                ?? throw new Exception("Wallet info is not exist");

            instance.AddSuccessLog(StepName);
        }
        catch (Exception ex)
        {
            instance.AddFailedLog(StepName, ex.Message);
        }

        return instance;
    }
}
