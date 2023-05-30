using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services.PaymentSchemes;
using ClearBank.DeveloperTest.Types;
using System.Configuration;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IDataStore _dataStore;

        public PaymentService()
        {
            var dataStoreType = ConfigurationManager.AppSettings["DataStoreType"];
            _dataStore = dataStoreType == "Backup" ? new BackupAccountDataStore() : new AccountDataStore();
        }

        public PaymentService(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var account = _dataStore.GetAccount(request.DebtorAccountNumber);
            var context = new PaymentContext();

            switch (request.PaymentScheme)
            {
                case PaymentScheme.Bacs:
                    context.SetScheme(new BacsScheme());
                    break;

                case PaymentScheme.FasterPayments:
                    context.SetScheme(new FasterPaymentsScheme());
                    break;

                case PaymentScheme.Chaps:
                    context.SetScheme(new ChapsScheme());
                    break;
            }

            var result = context.MakePayment(account, request);

            if (result.Success)
            {
                account.Balance -= request.Amount;
                _dataStore.UpdateAccount(account);
            }

            return result;
        }
    }
}
