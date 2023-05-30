using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public interface IPaymentScheme
    {
        MakePaymentResult MakePayment(Account account, MakePaymentRequest paymentRequest);
    }
}
