﻿using dhango.Web.Sdk.Api;
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
            // We are using a random number here just to create various transaction sizes.
            var amount = new Random().Next(10, 1500);
            var authorizeRequest = new PostAuthorizeRequest
            {
                Payer = "John Smith",
                UserId = "jsmith12345",
                EmailAddress = "jsmith@example.com",
                Card = GetCard(),
                Metadata = GetMetadata(),
                BillingAddress = GetAddress(),
                ShippingAddress = GetAddress(),
                Amount = amount,
                Currency = Currency.USD,
                Comments = "We are so excited about this purchase!",
            };
            var authorizeResponse = transactionsApi.TransactionsAuthorizePost(authorizeRequest, apiSettings.AccountKey);

            Assert.IsTrue(authorizeResponse.Id > 0);

            var getAuthorizeResponse = transactionsApi.TransactionsIdGet(authorizeResponse.Id, apiSettings.AccountKey);

            Assert.IsNotNull(getAuthorizeResponse);

            var captureAmount = Math.Round(amount * .4, 2);
            var captureResponse = transactionsApi.TransactionsIdCapturePost(authorizeResponse.Id
                , new PostCaptureRequest
                {
                    Amount = captureAmount,
                    PayerFee = Math.Round(amount * .03, 2),
                    PlatformFee = Math.Round(amount * .01, 2),
                }, apiSettings.AccountKey);

            Assert.IsTrue(captureResponse.Id > 0);
            Assert.IsNull(captureResponse.ErrorMessage);

            var getCaptureResponse = transactionsApi.TransactionsIdGet(captureResponse.Id, apiSettings.AccountKey);

            Assert.AreEqual(captureAmount, getCaptureResponse.Amount);
        }

        [TestMethod]
        public void AuthorizeAndCaptureWithLevel23ShouldWork()
        {
            var amount = 500;
            var authorizeRequest = new PostAuthorizeRequest
            {
                Payer = "John Smith",
                UserId = "jsmith12345",
                EmailAddress = "jsmith@example.com",
                Card = GetCard(),
                Metadata = GetMetadata(),
                BillingAddress = GetAddress(),
                ShippingAddress = GetAddress(),
                Amount = amount,
                Currency = Currency.USD,
                Comments = "We are so excited about this purchase!",

                CustomerId = "1234567890",
                OrderId = "abcdefghi",
            };
            var authorizeResponse = transactionsApi.TransactionsAuthorizePost(authorizeRequest, apiSettings.AccountKey);

            Assert.IsTrue(authorizeResponse.Id > 0);

            var getAuthorizeResponse = transactionsApi.TransactionsIdGet(authorizeResponse.Id, apiSettings.AccountKey);

            Assert.IsNotNull(getAuthorizeResponse);

            var captureAmount = 161.24d;
            var captureResponse = transactionsApi.TransactionsIdCapturePost(authorizeResponse.Id
                , new PostCaptureRequest
                {
                    Amount = captureAmount,
                    PayerFee = Math.Round(amount * .03, 2),
                    PlatformFee = Math.Round(amount * .01, 2),

                    TaxAmount = 40,
                    ShippingAmount = 15,
                    LineItems = new List<TransactionLineItem>
                    {
                        new TransactionLineItem
                        {
                            ProductCode = "widget123",
                            ProductDescription = "Shiny new widget.",
                            UnitPrice = 51.2,
                            Quantity = 3,
                            TaxAmount = .51,
                            DiscountAmount = 7.87,
                            UnitOfMeasure = "ea",
                            CommodityCode = "44121709",
                        }
                    }
                }, apiSettings.AccountKey);

            Assert.IsTrue(captureResponse.Id > 0);
            Assert.IsNull(captureResponse.ErrorMessage);

            var getCaptureResponse = transactionsApi.TransactionsIdGet(captureResponse.Id, apiSettings.AccountKey);

            Assert.AreEqual(captureAmount, getCaptureResponse.Amount);
        }

        [TestMethod]
        public void AuthorizeAndVoidShouldWork()
        {
            var amount = new Random().Next(10, 1500);
            var authorizeRequest = new PostAuthorizeRequest
            {
                Payer = "John Smith",
                UserId = "jsmith12345",
                EmailAddress = "jsmith@example.com",
                Card = GetCard(),
                Metadata = GetMetadata(),
                BillingAddress = GetAddress(),
                ShippingAddress = GetAddress(),
                Amount = amount,
                Currency = Currency.USD,
                Comments = "We are so excited about this purchase!",
            };
            var authorizeResponse = transactionsApi.TransactionsAuthorizePost(authorizeRequest, apiSettings.AccountKey);

            Assert.IsTrue(authorizeResponse.Id > 0);

            transactionsApi.TransactionsIdVoidPost(authorizeResponse.Id, apiSettings.AccountKey);

            var getResponse = transactionsApi.TransactionsIdGet(authorizeResponse.Id, apiSettings.AccountKey);

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
            var amount = new Random().Next(10, 1500);
            var authorizeRequest = new PostAuthorizeRequest
            {
                Payer = "John Smith",
                UserId = "jsmith12345",
                EmailAddress = "jsmith@example.com",
                Card = GetCard(),
                Metadata = GetMetadata(),
                BillingAddress = GetAddress(),
                ShippingAddress = GetAddress(),
                Amount = amount,
                Currency = Currency.USD,
                Comments = "We are so excited about this purchase!",
            };
            var authorizeResponse = transactionsApi.TransactionsAuthorizePost(authorizeRequest, apiSettings.AccountKey);

            Assert.IsNotNull(transactionsApi.TransactionsIdGet(authorizeResponse.Id, apiSettings.AccountKey));

            // The platform can access this transaction.
            Assert.IsNotNull(transactionsApi.TransactionsIdGet(authorizeResponse.Id));

            try
            {
                // Another account key should not be able to access this transaction.
                transactionsApi.TransactionsIdGet(authorizeResponse.Id, apiSettings.OtherAccountKey);

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
            var amount = new Random().Next(10, 1500);
            var request = new PostPayRequest
            {
                Payer = "John Smith",
                UserId = "jsmith12345",
                EmailAddress = "jsmith@example.com",
                Ach = GetAch(),
                Metadata = GetMetadata(),
                BillingAddress = GetAddress(),
                ShippingAddress = GetAddress(),
                Amount = amount,
                PayerFee = Math.Round(amount * .03, 2),
                PlatformFee = Math.Round(amount * .01, 2),
                Currency = Currency.USD,
                Comments = "We are so excited about this purchase!",
            };

            var postPayResponse = transactionsApi.TransactionsPayPost(request, apiSettings.AccountKey);
            var getResponse = transactionsApi.TransactionsIdGet(postPayResponse.Id, apiSettings.AccountKey);

            Assert.IsNotNull(getResponse);
            Assert.IsNull(getResponse.Events.SingleOrDefault(x => x.TransactionEventType == TransactionEventType.Reject));
        }

        [TestMethod]
        public void CreditCardPaymentShouldWork()
        {
            var amount = new Random().Next(10, 2000);
            var request = new PostPayRequest
            {
                Payer = "John Smith",
                UserId = "jsmith12345",
                EmailAddress = "jsmith@example.com",
                Card = GetCard(),
                Metadata = GetMetadata(),
                BillingAddress = GetAddress(),
                ShippingAddress = GetAddress(),
                Amount = amount,
                PayerFee = Math.Round(amount * .03, 2),
                PlatformFee = Math.Round(amount * .01, 2),
                Currency = Currency.USD,
                Comments = "We are so excited about this purchase!",
            };

            var postPayResponse = transactionsApi.TransactionsPayPost(request, apiSettings.AccountKey);
            var getResponse = transactionsApi.TransactionsIdGet(postPayResponse.Id, apiSettings.AccountKey);

            Assert.IsNotNull(getResponse);
            Assert.IsNull(getResponse.Events.SingleOrDefault(x => x.TransactionEventType == TransactionEventType.Reject));
        }

        [TestMethod]
        public void CreditCardPaymentWithLevel23ShouldWork()
        {
            var amount = 161.24d;
            var request = new PostPayRequest
            {
                Payer = "John Smith",
                UserId = "jsmith12345",
                EmailAddress = "jsmith@example.com",
                Card = GetCard(),
                Metadata = GetMetadata(),
                BillingAddress = GetAddress(),
                ShippingAddress = GetAddress(),
                Amount = amount,
                PayerFee = Math.Round(amount * .03, 2),
                PlatformFee = Math.Round(amount * .01, 2),
                Currency = Currency.USD,
                Comments = "We are so excited about this purchase!",

                CustomerId = "1234567890",
                OrderId = "abcdefghi",
                TaxAmount = 40,
                ShippingAmount = 15,
                LineItems = new List<TransactionLineItem>
                {
                    new TransactionLineItem
                    {
                        ProductCode = "widget123",
                        ProductDescription = "Shiny new widget.",
                        UnitPrice = 51.2,
                        Quantity = 3,
                        TaxAmount = .51,
                        DiscountAmount = 7.87,
                        UnitOfMeasure = "ea",
                        CommodityCode = "44121709",
                    }
                }
            };

            var postPayResponse = transactionsApi.TransactionsPayPost(request, apiSettings.AccountKey);
            var getResponse = transactionsApi.TransactionsIdGet(postPayResponse.Id, apiSettings.AccountKey);

            Assert.IsNotNull(getResponse);
            Assert.IsNull(getResponse.Events.SingleOrDefault(x => x.TransactionEventType == TransactionEventType.Reject));
        }
    }
}
