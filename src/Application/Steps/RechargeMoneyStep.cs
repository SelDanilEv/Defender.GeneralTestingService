using Defender.GeneralTestingService.Application.Helpers;
using Defender.GeneralTestingService.Application.Models;
using Defender.GeneralTestingService.Application.Steps.Interfaces;
using Defender.GeneralTestingService.Domain.Enums;
using Defender.GeneralTestingService.Application.Clients.Portal;

namespace Defender.GeneralTestingService.Application.Steps;

public class RechargeMoneyStep(IPortalWrapper portalWrapper) : IParallelStep
{
    private const string StepName = "Recharge money";

    public async Task<TestInstance> StartAsync(TestInstance instance)
    {
        try
        {
            var currentWalletInfo = await portalWrapper.GetWalletInfoAsync();

            var transaction = await portalWrapper.RechargeMoneyAsync(
                currentWalletInfo.WalletNumber,
                1,
                Currency.USD);

            await TransactionsHelper.VerifyTransaction(transaction.TransactionId, portalWrapper);

            instance.AddSuccessLog(StepName);
        }
        catch (Exception ex)
        {
            instance.AddFailedLog(StepName, ex.Message);
        }

        return instance;
    }
}
