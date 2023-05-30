using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class FasterPaymentsScheme : IPaymentScheme
    {
        public MakePaymentResult MakePayment(Account account, MakePaymentRequest request)
        {
            var success = true;

            if (account == null)
                success = false;
            if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments))
                success = false;
            if (account.Balance < request.Amount)
                success = false;

            return new MakePaymentResult()
            {
                Success = success
            };
        }
    }
}
