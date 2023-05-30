using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services.PaymentSchemes
{
    public class BacsScheme : IPaymentScheme
    {
        public MakePaymentResult MakePayment(Account account, MakePaymentRequest request)
        {
            var success = true;

            if (account == null)
                success = false;
            if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs))
                success = false;

            return new MakePaymentResult()
            {
                Success = success
            };
        }
    }
}
