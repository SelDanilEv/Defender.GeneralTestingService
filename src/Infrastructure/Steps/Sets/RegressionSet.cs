using Defender.GeneralTestingService.Infrastructure.Steps.Interfaces;

namespace Defender.GeneralTestingService.Infrastructure.Steps.Sets;

public class RegressionSet : List<IStep>
{
    public RegressionSet(
               LoginStep loginStep,
               VerifyWalletStep verifyWalletStep,
               TransferMoneyStep transferMoneyStep)
    {
        Add(loginStep);
        Add(verifyWalletStep);
        Add(transferMoneyStep);
    }
}
