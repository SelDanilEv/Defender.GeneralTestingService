using Defender.Common.Clients.Portal;
using Defender.GeneralTestingService.Application.Models;
using Defender.GeneralTestingService.Infrastructure.Clients.Portal;
using Defender.GeneralTestingService.Infrastructure.Helpers.LocalSecretHelper;
using Defender.GeneralTestingService.Infrastructure.Steps.Interfaces;

namespace Defender.GeneralTestingService.Infrastructure.Steps;

public class TransferMoneyStep : IParallelStep
{
    private const string StepName = "Transfer money";

    private readonly IPortalWrapper _portalWrapper;

    public TransferMoneyStep(IPortalWrapper portalWrapper)
    {
        _portalWrapper = portalWrapper;
    }

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

            var transaction = await _portalWrapper.TransferMoneyAsync(
                walletNumber,
                1,
                Currency.USD);

            bool transactionSucceeded = false;

            for (int i = 0; i < 3; i++)
            {
                var latestTransaction = await _portalWrapper.GetLatestTransactionAsync();

                if (latestTransaction.TransactionId == transaction.TransactionId
                    && latestTransaction.TransactionStatus == TransactionStatus.Proceed)
                {
                    transactionSucceeded = true;
                    break;
                }

                await Task.Delay(1000);
            }

            if (!transactionSucceeded)
            {
                throw new Exception("Transaction was not proceed or founded");
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
