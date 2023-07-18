using dhango.Web.Sdk.Api;
using dhango.Web.Sdk.Client;
using dhango.Web.Sdk.Model;

namespace dhango.Web.Sdk.Tests
{
    [TestClass]
    public class TokenTests : TestBase
    {
        private ApiSettings apiSettings = null!;
        private TokensApi tokensApi = null!;
        private TransactionsApi transactionsApi = null!;

        [TestInitialize]
        public void Initialize()
        {
            apiSettings = GetApiSettings();

            tokensApi = new TokensApi(apiSettings.BaseUrl,
                apiSettings.Key,
                apiSettings.Secret);

            transactionsApi = new TransactionsApi(apiSettings.BaseUrl,
                apiSettings.Key,
                apiSettings.Secret);
        }

        [TestMethod]
        public void ShouldBeAbleToCreateGetAndDeleteToken()
        {
            var postTokenRequest = CreatePostTokenRequestForCard();
            var postTokenResponse = tokensApi.TokensPost(postTokenRequest);

            Assert.IsNotNull(postTokenResponse.Id);

            var getTokenResponse = tokensApi.TokensIdGet(postTokenResponse.Id);

            Assert.IsNotNull(getTokenResponse);

            tokensApi.TokensIdDelete(postTokenResponse.Id);

            try
            {
                tokensApi.TokensIdGet(postTokenResponse.Id);

                Assert.Fail();
            }
            catch (ApiException ex)
            {
                Assert.AreEqual(404, ex.ErrorCode);
            }
        }

        [TestMethod]
        public void UsingOnlyAnAccountKeyShouldAllowCreationOnly()
        {
            var accountKeytokensApi = new TokensApi(apiSettings.BaseUrl,
                null!,
                null!);

            var postTokenRequest = CreatePostTokenRequestForCard();
            var postTokenResponse = accountKeytokensApi.TokensPost(postTokenRequest, apiSettings.AccountKey);

            Assert.IsNotNull(postTokenResponse.Id);

            try
            {
                // Using only an account key should not allow other operations.
                accountKeytokensApi.TokensIdDelete(postTokenResponse.Id, apiSettings.AccountKey);

                Assert.Fail();
            }
            catch (ApiException ex)
            {
                Assert.AreEqual(401, ex.ErrorCode);
            }

            // Other operations still require the key and secret.
            var getTokenResponse = tokensApi.TokensIdGet(postTokenResponse.Id, apiSettings.AccountKey);

            Assert.IsNotNull(getTokenResponse);
        }

        [TestMethod]
        public void UsingCardTokenForPaymentShouldBeSuccessful()
        {
            var postTokenRequest = CreatePostTokenRequestForCard();
            var postTokenResponse = tokensApi.TokensPost(postTokenRequest);
            
            var postPayRequest = CreatePostPayRequestWithTokenId(postTokenResponse.Id!);

            var postPayResponse = transactionsApi.TransactionsPayPost(postPayRequest);
            var getResponse = transactionsApi.TransactionsIdGet(postPayResponse.Id);

            Assert.IsNotNull(getResponse);
            Assert.IsNull(getResponse.Events.SingleOrDefault(x => x.TransactionEventType == TransactionEventType.Reject));
        }

        [TestMethod]
        public void UsingAchTokenForPaymentShouldBeSuccessful()
        {
            var postTokenRequest = CreatePostTokenRequestForAch();
            var postTokenResponse = tokensApi.TokensPost(postTokenRequest);

            var postPayRequest = CreatePostPayRequestWithTokenId(postTokenResponse.Id!);

            var postPayResponse = transactionsApi.TransactionsPayPost(postPayRequest);
            var getResponse = transactionsApi.TransactionsIdGet(postPayResponse.Id);

            Assert.IsNotNull(getResponse);
            Assert.IsNull(getResponse.Events.SingleOrDefault(x => x.TransactionEventType == TransactionEventType.Reject));
        }

        [TestMethod]
        public void UsingCardTokenForAuthorizationShouldBeSuccessful()
        {
            var amount = new Random().Next(10, 1500);
            var postTokenRequest = CreatePostTokenRequestForCard();
            var postTokenResponse = tokensApi.TokensPost(postTokenRequest);

            var authorizeRequest = new PostAuthorizeRequest
            {
                Payer = "John Smith",
                UserId = "jsmith12345",
                EmailAddress = "jsmith@example.com",
                TokenId = postTokenResponse.Id,
                Metadata = GetMetadata(),
                BillingAddress = GetAddress(),
                ShippingAddress = GetAddress(),
                Amount = amount,
                Currency = Currency.USD,
                Comments = "We are so excited about this purchase!",
            };
            var authorizeResponse = transactionsApi.TransactionsAuthorizePost(authorizeRequest);

            Assert.IsTrue(authorizeResponse.Success);
        }

        [TestMethod]
        public void UsingAchTokenForAuthorizationShouldFail()
        {
            var amount = new Random().Next(10, 1500);
            var postTokenRequest = CreatePostTokenRequestForAch();
            var postTokenResponse = tokensApi.TokensPost(postTokenRequest);

            var authorizeRequest = new PostAuthorizeRequest
            {
                Payer = "John Smith",
                UserId = "jsmith12345",
                EmailAddress = "jsmith@example.com",
                TokenId = postTokenResponse.Id,
                Metadata = GetMetadata(),
                BillingAddress = GetAddress(),
                ShippingAddress = GetAddress(),
                Amount = amount,
                Currency = Currency.USD,
                Comments = "We are so excited about this purchase!",
            };

            try
            {
                var authorizeResponse = transactionsApi.TransactionsAuthorizePost(authorizeRequest);

                Assert.Fail();
            }
            catch(ApiException exception)
            {
                Assert.AreEqual(400, exception.ErrorCode);
            }
        }

        // This is an example of how to share tokens. The token can be created at the platform level
        // and used across all accounts under the platform.
        [TestMethod]
        public void PlatformTokenOnMerchantTransactionShouldWork()
        {
            var postTokenRequest = CreatePostTokenRequestForAch();
            var postTokenResponse = tokensApi.TokensPost(postTokenRequest);

            var postPayRequest = CreatePostPayRequestWithTokenId(postTokenResponse.Id!);
            var postPayResponse = transactionsApi.TransactionsPayPost(postPayRequest, apiSettings.AccountKey);
        }

        [TestMethod]
        public void MerchantTokenOnPlatformTransactionShouldNotWork()
        {
            var postTokenRequest = CreatePostTokenRequestForAch();
            var postTokenResponse = tokensApi.TokensPost(postTokenRequest, apiSettings.AccountKey);

            try
            {
                var postPayRequest = CreatePostPayRequestWithTokenId(postTokenResponse.Id!);
                var postPayResponse = transactionsApi.TransactionsPayPost(postPayRequest);

                Assert.Fail();
            }
            catch (ApiException exception)
            {
                Assert.AreEqual(400, exception.ErrorCode);
            }
        }

        [TestMethod]
        public void PaymentWithoutAddressShouldUseTokenBillingAddress()
        {
            var postTokenRequest = CreatePostTokenRequestForAch();
            var postTokenResponse = tokensApi.TokensPost(postTokenRequest);

            var postPayRequest = CreatePostPayRequestWithTokenId(postTokenResponse.Id!);

            postPayRequest.BillingAddress = null!;
            postPayRequest.ShippingAddress = null!;

            var postPayResponse = transactionsApi.TransactionsPayPost(postPayRequest);

            var getResponse = transactionsApi.TransactionsIdGet(postPayResponse.Id);

            Assert.IsNotNull(getResponse.BillingAddress);
            Assert.IsNull(getResponse.ShippingAddress);
        }

        private PostTokenRequest CreatePostTokenRequestForCard()
        {
            return new PostTokenRequest
            {
                UserId = "jsmith12345",
                EmailAddress = "jsmith@example.com",
                Card = GetCard(),
                Metadata = GetMetadata(),
                Address = GetAddress()
            };
        }

        private PostTokenRequest CreatePostTokenRequestForAch()
        {
            return new PostTokenRequest
            {
                UserId = "jsmith12345",
                EmailAddress = "jsmith@example.com",
                Ach = GetAch(),
                Metadata = GetMetadata(),
                Address = GetAddress()
            };
        }

        private PostPayRequest CreatePostPayRequestWithTokenId(string id)
        {
            var amount = new Random().Next(1000, 150000);

            return new PostPayRequest
            {
                Payer = "John Smith",
                UserId = "jsmith12345",
                EmailAddress = "jsmith@example.com",
                TokenId = id,
                Metadata = GetMetadata(),
                BillingAddress = GetAddress(),
                ShippingAddress = GetAddress(),
                Amount = amount,
                PayerFee = (long)Math.Round(amount * .03, 0),
                PlatformFee = (long)Math.Round(amount * .01, 0),
                Currency = Currency.USD,
                Comments = "We are so excited about this purchase!",
            };
        }
    }
}
