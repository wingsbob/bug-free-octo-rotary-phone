using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services.PaymentSchemes
{
    public class DefaultScheme : IPaymentScheme
    {
        public MakePaymentResult MakePayment(Account account, MakePaymentRequest request)
        {
            return new MakePaymentResult()
            {
                Success = false
            };
        }
    }
}
