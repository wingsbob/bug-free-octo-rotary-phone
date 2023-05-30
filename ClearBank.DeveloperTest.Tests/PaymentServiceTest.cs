using Xunit;
using FluentAssertions;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Moq;
using ClearBank.DeveloperTest.Data;

namespace ClearBank.DeveloperTest.UnitTests.Services
{
    public class PaymentService_MakePaymentShould
    {
        [Fact]
        public void MakePayment_WhenUsingBacs_ShouldSucceed()
        {
            var accountNumber = "123";
            var dataStoreMock = new Mock<IDataStore>();

            dataStoreMock.Setup(x => x.GetAccount(accountNumber)).Returns(new Account()
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs,
            });

            var primeService = new PaymentService(dataStoreMock.Object);
            var result = primeService.MakePayment(new MakePaymentRequest()
            {
                PaymentScheme = PaymentScheme.Bacs,
                DebtorAccountNumber = accountNumber,
            });

            result.Should().BeEquivalentTo(new MakePaymentResult() { Success = true });
        }

        [Fact]
        public void MakePayment_WhenUsingBacs_ShouldSucceedAndDeductFromAccount()
        {
            var accountNumber = "123";
            var account = new Account()
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs,
                Balance = 100,
            };
            var dataStoreMock = new Mock<IDataStore>();

            dataStoreMock.Setup(x => x.GetAccount(accountNumber)).Returns(account);

            var primeService = new PaymentService(dataStoreMock.Object);
            var result = primeService.MakePayment(new MakePaymentRequest()
            {
                PaymentScheme = PaymentScheme.Bacs,
                DebtorAccountNumber = accountNumber,
                Amount = 10,
            });

            result.Success.Should().Be(true);
            dataStoreMock.Verify(m => m.UpdateAccount(account), Times.Exactly(1));
            account.Balance.Should().Be(90);
        }

        [Fact]
        public void MakePayment_WhenUsingBacs_Should_FailWhenUnsupported()
        {
            var accountNumber = "123";
            var dataStoreMock = new Mock<IDataStore>();

            dataStoreMock.Setup(x => x.GetAccount(accountNumber)).Returns(new Account());

            var primeService = new PaymentService(dataStoreMock.Object);
            var result = primeService.MakePayment(new MakePaymentRequest()
            {
                PaymentScheme = PaymentScheme.Bacs,
                DebtorAccountNumber = accountNumber,
            });

            result.Should().BeEquivalentTo(new MakePaymentResult() { Success = false });
        }

        [Fact]
        public void MakePayment_WhenUsingFasterPayments_ShouldSucceed()
        {
            var accountNumber = "123";
            var dataStoreMock = new Mock<IDataStore>();

            dataStoreMock.Setup(x => x.GetAccount(accountNumber)).Returns(new Account()
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
            });

            var primeService = new PaymentService(dataStoreMock.Object);
            var result = primeService.MakePayment(new MakePaymentRequest()
            {
                PaymentScheme = PaymentScheme.FasterPayments,
                DebtorAccountNumber = accountNumber,
            });

            result.Should().BeEquivalentTo(new MakePaymentResult() { Success = true });
        }

        [Fact]
        public void MakePayment_WhenUsingFasterPayments_ShouldSucceedAndDeductFromAccount()
        {
            var accountNumber = "123";
            var account = new Account()
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                Balance = 100,
            };
            var dataStoreMock = new Mock<IDataStore>();

            dataStoreMock.Setup(x => x.GetAccount(accountNumber)).Returns(account);

            var primeService = new PaymentService(dataStoreMock.Object);
            var result = primeService.MakePayment(new MakePaymentRequest()
            {
                PaymentScheme = PaymentScheme.FasterPayments,
                DebtorAccountNumber = accountNumber,
                Amount = 10,
            });

            result.Success.Should().Be(true);
            dataStoreMock.Verify(m => m.UpdateAccount(account), Times.Exactly(1));
            account.Balance.Should().Be(90);
        }

        [Fact]
        public void MakePayment_WhenUsingFasterPayments_ShouldFail_WhenUnsupported()
        {
            var accountNumber = "123";
            var dataStoreMock = new Mock<IDataStore>();

            dataStoreMock.Setup(x => x.GetAccount(accountNumber)).Returns(new Account());

            var primeService = new PaymentService(dataStoreMock.Object);
            var result = primeService.MakePayment(new MakePaymentRequest()
            {
                PaymentScheme = PaymentScheme.FasterPayments,
                DebtorAccountNumber = accountNumber,
            });

            result.Should().BeEquivalentTo(new MakePaymentResult() { Success = false });
        }

        [Fact]
        public void MakePayment_WhenUsingFasterPayments_ShouldFail_WhenBalanceIsTooLow()
        {
            var accountNumber = "123";
            var dataStoreMock = new Mock<IDataStore>();

            dataStoreMock.Setup(x => x.GetAccount(accountNumber)).Returns(new Account() { Balance = 0 });

            var primeService = new PaymentService(dataStoreMock.Object);
            var result = primeService.MakePayment(new MakePaymentRequest()
            {
                PaymentScheme = PaymentScheme.FasterPayments,
                DebtorAccountNumber = accountNumber,
                Amount = 10,
            });

            result.Should().BeEquivalentTo(new MakePaymentResult() { Success = false });
        }

        [Fact]
        public void MakePayment_WhenUsingChaps_ShouldSucceed()
        {
            var accountNumber = "123";
            var dataStoreMock = new Mock<IDataStore>();

            dataStoreMock.Setup(x => x.GetAccount(accountNumber)).Returns(new Account()
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
            });

            var primeService = new PaymentService(dataStoreMock.Object);
            var result = primeService.MakePayment(new MakePaymentRequest()
            {
                PaymentScheme = PaymentScheme.Chaps,
                DebtorAccountNumber = accountNumber,
            });

            result.Should().BeEquivalentTo(new MakePaymentResult() { Success = true });
        }

        [Fact]
        public void MakePayment_WhenUsingChaps_ShouldSucceedAndDeductFromAccount()
        {
            var accountNumber = "123";
            var account = new Account()
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                Balance = 100,
            };
            var dataStoreMock = new Mock<IDataStore>();

            dataStoreMock.Setup(x => x.GetAccount(accountNumber)).Returns(account);

            var primeService = new PaymentService(dataStoreMock.Object);
            var result = primeService.MakePayment(new MakePaymentRequest()
            {
                PaymentScheme = PaymentScheme.Chaps,
                DebtorAccountNumber = accountNumber,
                Amount = 10,
            });

            result.Success.Should().Be(true);
            dataStoreMock.Verify(m => m.UpdateAccount(account), Times.Exactly(1));
            account.Balance.Should().Be(90);
        }

        [Fact]
        public void MakePayment_WhenUsingChaps_ShouldFail_WhenUnsupported()
        {
            var accountNumber = "123";
            var dataStoreMock = new Mock<IDataStore>();

            dataStoreMock.Setup(x => x.GetAccount(accountNumber)).Returns(new Account());

            var primeService = new PaymentService(dataStoreMock.Object);
            var result = primeService.MakePayment(new MakePaymentRequest()
            {
                PaymentScheme = PaymentScheme.Chaps,
                DebtorAccountNumber = accountNumber,
            });

            result.Should().BeEquivalentTo(new MakePaymentResult() { Success = false });
        }

        [Fact]
        public void MakePayment_WhenUsingChaps_ShouldFail_WhenAccountIsInboundPaymentsOnly()
        {
            var accountNumber = "123";
            var dataStoreMock = new Mock<IDataStore>();

            dataStoreMock.Setup(x => x.GetAccount(accountNumber)).Returns(new Account() { Status = AccountStatus.InboundPaymentsOnly });

            var primeService = new PaymentService(dataStoreMock.Object);
            var result = primeService.MakePayment(new MakePaymentRequest()
            {
                PaymentScheme = PaymentScheme.Chaps,
                DebtorAccountNumber = accountNumber,
                Amount = 10,
            });

            result.Should().BeEquivalentTo(new MakePaymentResult() { Success = false });
        }

        [Fact]
        public void MakePayment_WhenUsingChaps_ShouldFail_WhenAccountIsDisabled()
        {
            var accountNumber = "123";
            var dataStoreMock = new Mock<IDataStore>();

            dataStoreMock.Setup(x => x.GetAccount(accountNumber)).Returns(new Account() { Status = AccountStatus.Disabled });

            var primeService = new PaymentService(dataStoreMock.Object);
            var result = primeService.MakePayment(new MakePaymentRequest()
            {
                PaymentScheme = PaymentScheme.Chaps,
                DebtorAccountNumber = accountNumber,
                Amount = 10,
            });

            result.Should().BeEquivalentTo(new MakePaymentResult() { Success = false });
        }
    }
}
