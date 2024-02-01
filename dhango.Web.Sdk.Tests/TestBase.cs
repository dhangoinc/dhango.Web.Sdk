using dhango.Web.Sdk.Model;
using Microsoft.Extensions.Configuration;

namespace dhango.Web.Sdk.Tests
{
    public abstract class TestBase
    {
        internal ApiSettings GetApiSettings()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var apiSettings = new ApiSettings();
            var configurationSection = configuration.GetSection("ApiSettings");

            return new ApiSettings
            {
                BaseUrl = configurationSection["baseUrl"],
                Key = configurationSection["key"],
                Secret = configurationSection["secret"],
                AccountKey = configurationSection["accountKey"],
                OtherAccountKey = configurationSection["otherAccountKey"],
            };
        }

        protected Card GetCard()
        {
            return new Card
            {
                CardHolder = "John Smith",
                CardNumber = "4000620000000007",
                ExpirationMonth = 3,
                ExpirationYear = 2030,
                SecurityCode = "737"
            };
        }

        protected Ach GetAch()
        {
            return new Ach
            {
                BankAccountHolder = "Acme Corporation",
                BankAccountType = BankAccountType.CorporateSavings,
                RoutingNumber = "111000025",
                AccountNumber = "000111111113"
            };
        }

        protected Address GetAddress()
        {
            return new Address
            {
                Name = "Acme Corporation",
                StreetAddress = "1234 Main Street",
                City = "Austin",
                StateOrProvince = "TX",
                PostalCode = "78701",
                Country = "US"
            };
        }

        protected Dictionary<string, string> GetMetadata()
        {
            return new Dictionary<string, string>()
                {
                    { "userId", "jsmith" },
                    { "accountId", "123" }
                };
        }
    }
}
