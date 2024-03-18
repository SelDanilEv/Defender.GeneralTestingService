namespace Defender.GeneralTestingService.Infrastructure.Steps.Sets;

public class RegressionSet : List<IStep>
{
    public RegressionSet(
               LoginStep loginStep,
               VerifyWalletStep verifyWalletStep)
    {
        Add(loginStep);
        Add(verifyWalletStep);
    }
}
