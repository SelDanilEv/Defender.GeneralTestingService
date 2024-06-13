using Defender.GeneralTestingService.Application.Steps.Interfaces;

namespace Defender.GeneralTestingService.Application.Steps.Sets;

public class RegressionSet : List<IStep>
{
    public RegressionSet(
               LoginStep loginStep,
               VerifyWalletStep verifyWalletStep,
               TransferMoneyStep transferMoneyStep,
               RechargeMoneyStep rechargeMoneyStep)
    {
        Add(loginStep);
        Add(verifyWalletStep);
        Add(rechargeMoneyStep);
        Add(transferMoneyStep);
    }
}
