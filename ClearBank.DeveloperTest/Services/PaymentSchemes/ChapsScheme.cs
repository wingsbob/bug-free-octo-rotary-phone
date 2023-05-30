using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class ChapsScheme : IPaymentScheme
    {
        public MakePaymentResult MakePayment(Account account, MakePaymentRequest request)
        {
            var success = true;

            if (account == null)
                success = false;
            if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps))
                success = false;
            if (account.Status != AccountStatus.Live)
                success = false;

            return new MakePaymentResult()
            {
                Success = success
            };
        }
    }
}
