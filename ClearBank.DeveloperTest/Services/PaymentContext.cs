using ClearBank.DeveloperTest.Services.PaymentSchemes;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentContext
    {
        private IPaymentScheme _paymentScheme = new DefaultScheme();

        public void SetScheme(IPaymentScheme scheme) => _paymentScheme = scheme;
        public MakePaymentResult MakePayment(Account account, MakePaymentRequest paymentRequest) =>
            _paymentScheme.MakePayment(account, paymentRequest);
    }
}
