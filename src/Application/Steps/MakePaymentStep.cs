using Defender.GeneralTestingService.Application.Clients.Portal;
using Defender.GeneralTestingService.Application.Helpers;
using Defender.GeneralTestingService.Application.Helpers.LocalSecretHelper;
using Defender.GeneralTestingService.Application.Models;
using Defender.GeneralTestingService.Application.Steps.Interfaces;
using Defender.GeneralTestingService.Domain.Enums;

namespace Defender.GeneralTestingService.Application.Steps;

public class MakePaymentStep(IPortalWrapper portalWrapper) : IParallelStep
{
    private const string StepName = "Payment";

    public async Task<TestInstance> StartAsync(TestInstance instance)
    {
        try
        {
            if (!int.TryParse(
                await LocalSecretsHelper.GetSecretAsync(
                    LocalSecret.Testing_TransferWalletNumber),
                out var walletNumber))
            {
                throw new Exception("No valid target wallet number");
            };

            var transaction = await portalWrapper.TransferMoneyAsync(
                walletNumber,
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
