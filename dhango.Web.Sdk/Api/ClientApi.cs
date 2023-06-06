using dhango.Web.Sdk.Client;

namespace dhango.Web.Sdk.Api
{
    public abstract class ClientApi
    {
        private dhango.Web.Sdk.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Gets or sets the configuration object
        /// </summary>
        /// <value>An instance of the Configuration</value>
        public dhango.Web.Sdk.Client.Configuration Configuration { get; set; }

        /// <summary>
        /// Provides a factory method hook for the creation of exceptions.
        /// </summary>
        public dhango.Web.Sdk.Client.ExceptionFactory ExceptionFactory
        {
            get
            {
                if (_exceptionFactory != null && _exceptionFactory.GetInvocationList().Length > 1)
                {
                    throw new InvalidOperationException("Multicast delegate for ExceptionFactory is unsupported.");
                }
                return _exceptionFactory;
            }
            set { _exceptionFactory = value; }
        }

        public ClientApi(String basePath, string key, string secret)
        {
            this.Configuration = new dhango.Web.Sdk.Client.Configuration { BasePath = basePath };

            AddAuthorizationHeader(this.Configuration, key, secret);

            ExceptionFactory = dhango.Web.Sdk.Client.Configuration.DefaultExceptionFactory;
        }

        protected void AddAuthorizationHeader(Configuration configuration, string key, string secret)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(key + ":" + secret);
            var headerValue = "Basic " + System.Convert.ToBase64String(plainTextBytes);

            configuration.AddDefaultHeader("Authorization", headerValue);
        }
    }
}
