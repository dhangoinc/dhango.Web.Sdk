using dhango.Web.Sdk.Api;
using dhango.Web.Sdk.Client;
using dhango.Web.Sdk.Model;

namespace dhango.Web.Sdk.Tests
{
    [TestClass]
    public class TransactionTests : TestBase
    {
        private ApiSettings apiSettings = null!;
        private TransactionsApi transactionsApi = null!;

        [TestInitialize]
        public void Initialize()
        {
            apiSettings = GetApiSettings();

            transactionsApi = new TransactionsApi(apiSettings.BaseUrl,
                apiSettings.Key,
                apiSettings.Secret);
        }

        [TestMethod]
        public void AuthorizeAndCaptureShouldWork()
        {
            var authorizeRequest = new PostAuthorizeRequest
            {
                Payer = "John Smith",
                UserId = "jsmith12345",
                EmailAddress = "jsmith@example.com",
                Card = GetCard(),
                Metadata = GetMetadata(),
                BillingAddress = GetAddress(),
                ShippingAddress = GetAddress(),
                Amount = 787.33,
                PayerFee = 5.12,
                PlatformFee = 5.12,
                Currency = Currency.USD,
                Comments = "We are so excited about this purchase!",
            };
            var authorizeResponse = transactionsApi.TransactionsAuthorizePost(authorizeRequest);

            Assert.IsTrue(authorizeResponse.Id > 0);

            var getAuthorizeResponse = transactionsApi.TransactionsIdGet(authorizeResponse.Id);

            Assert.IsNotNull(getAuthorizeResponse);

            var captureResponse = transactionsApi.TransactionsIdCapturePost(authorizeResponse.Id
                , new PostCaptureRequest { Amount = 234.11 });

            Assert.IsTrue(captureResponse.Id > 0);

            var getCaptureResponse = transactionsApi.TransactionsIdGet(captureResponse.Id);

            Assert.IsTrue(captureResponse.Success);
            Assert.AreEqual(234.11, getCaptureResponse.Amount);
        }

        [TestMethod]
        public void AuthorizeAndVoidShouldWork()
        {
            var authorizeRequest = new PostAuthorizeRequest
            {
                Payer = "John Smith",
                UserId = "jsmith12345",
                EmailAddress = "jsmith@example.com",
                Card = GetCard(),
                Metadata = GetMetadata(),
                BillingAddress = GetAddress(),
                ShippingAddress = GetAddress(),
                Amount = 787.33,
                PayerFee = 5.12,
                PlatformFee = 5.12,
                Currency = Currency.USD,
                Comments = "We are so excited about this purchase!",
            };
            var authorizeResponse = transactionsApi.TransactionsAuthorizePost(authorizeRequest);

            Assert.IsTrue(authorizeResponse.Id > 0);

            transactionsApi.TransactionsIdVoidPost(authorizeResponse.Id);

            var getResponse = transactionsApi.TransactionsIdGet(authorizeResponse.Id);

            Assert.IsNotNull(getResponse.Events.SingleOrDefault(x => x.TransactionEventType == TransactionEventType.Void));
        }

        [TestMethod]
        public void GetInvalidTransactionShouldReturn404()
        {
            try
            {
                transactionsApi.TransactionsIdGet(52901922);

                Assert.Fail();
            }
            catch (ApiException ex)
            {
                Assert.AreEqual(404, ex.ErrorCode);
            }
        }

        [TestMethod]
        public void ShouldRequireMerchantKeyToAccessMerchantTransaction()
        {
            var authorizeRequest = new PostAuthorizeRequest
            {
                Payer = "John Smith",
                UserId = "jsmith12345",
                EmailAddress = "jsmith@example.com",
                Card = GetCard(),
                Metadata = GetMetadata(),
                BillingAddress = GetAddress(),
                ShippingAddress = GetAddress(),
                Amount = 787.33,
                PayerFee = 5.12,
                PlatformFee = 5.12,
                Currency = Currency.USD,
                Comments = "We are so excited about this purchase!",
            };
            var authorizeResponse = transactionsApi.TransactionsAuthorizePost(authorizeRequest, apiSettings.MerchantKey);

            Assert.IsNotNull(transactionsApi.TransactionsIdGet(authorizeResponse.Id, apiSettings.MerchantKey));

            try
            {
                transactionsApi.TransactionsIdGet(authorizeResponse.Id);

                Assert.Fail();
            }
            catch (ApiException ex)
            {
                Assert.AreEqual(404, ex.ErrorCode);
            }
        }

        [TestMethod]
        public void AchPaymentShouldWork()
        {
            var request = new PostPayRequest
            {
                Payer = "John Smith",
                UserId = "jsmith12345",
                EmailAddress = "jsmith@example.com",
                Ach = GetAch(),
                Metadata = GetMetadata(),
                BillingAddress = GetAddress(),
                ShippingAddress = GetAddress(),
                Amount = 787.33,
                PayerFee = 5.12,
                PlatformFee = 5.12,
                Currency = Currency.USD,
                Comments = "We are so excited about this purchase!",
            };

            var postPayResponse = transactionsApi.TransactionsPayPost(request);
            var getResponse = transactionsApi.TransactionsIdGet(postPayResponse.Id);

            Assert.IsNotNull(getResponse);
            Assert.IsNull(getResponse.Events.SingleOrDefault(x => x.TransactionEventType == TransactionEventType.Reject));
        }

        [TestMethod]
        public void CreditCardPaymentShouldWork()
        {
            var request = new PostPayRequest
            {
                Payer = "John Smith",
                UserId = "jsmith12345",
                EmailAddress = "jsmith@example.com",
                Card = GetCard(),
                Metadata = GetMetadata(),
                BillingAddress = GetAddress(),
                ShippingAddress = GetAddress(),
                Amount = 787.33,
                PayerFee = 5.12,
                PlatformFee = 5.12,
                Currency = Currency.USD,
                Comments = "We are so excited about this purchase!",
            };

            var postPayResponse = transactionsApi.TransactionsPayPost(request);

            var getResponse = transactionsApi.TransactionsIdGet(postPayResponse.Id);

            Assert.IsNotNull(getResponse);
            Assert.IsNull(getResponse.Events.SingleOrDefault(x => x.TransactionEventType == TransactionEventType.Reject));
        }
    }
}
