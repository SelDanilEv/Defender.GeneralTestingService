using Defender.Common.Clients.Portal;
using Defender.GeneralTestingService.Application.Clients.Portal;

namespace Defender.GeneralTestingService.Application.Helpers;

public class TransactionsHelper
{
    public static async Task VerifyTransaction(string transactionId, IPortalWrapper portalWrapper)
    {
        bool transactionSucceeded = false;
        bool transactionFound = false;
        var latestTransaction = new PortalTransactionDto();

        for (int i = 0; i < 5; i++)
        {
            var latestTransactions = await portalWrapper.GetLatest50TransactionsAsync();

            latestTransaction = latestTransactions.FirstOrDefault(t => t.TransactionId == transactionId);

            if (latestTransaction != null)
            {
                transactionFound = true;

                if (latestTransaction.TransactionStatus == PortalTransactionDtoTransactionStatus.Proceed)
                {
                    transactionSucceeded = true;
                    break;
                }
            }

            await Task.Delay(1000);
        }

        if (!transactionFound)
        {
            throw new Exception("Transaction was not founded");
        }
        else if (!transactionSucceeded)
        {
            throw new Exception($"Transaction was not proceed. Last status: {latestTransaction.TransactionStatus}");
        }
    }
}
