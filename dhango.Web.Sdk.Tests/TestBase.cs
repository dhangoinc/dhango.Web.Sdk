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
            };
        }

        protected Card GetCard()
        {
            return new Card
            {
                CardHolder = "John Smith",
                CardNumber = "4895379980003055",
                ExpirationMonth = 12,
                ExpirationYear = 2025,
                SecurityCode = "123"
            };
        }

        protected Ach GetAch()
        {
            return new Ach
            {
                BankAccountHolder = "Acme Corporation",
                RoutingNumber = "021000021",
                AccountNumber = "08292983191"
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
