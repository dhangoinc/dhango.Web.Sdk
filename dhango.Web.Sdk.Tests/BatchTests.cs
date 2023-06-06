using dhango.Web.Sdk.Api;
using dhango.Web.Sdk.Client;

namespace dhango.Web.Sdk.Tests
{
    [TestClass]
    public class BatchTests : TestBase
    {
        private ApiSettings apiSettings = null!;
        private BatchesApi batchesApi = null!;

        [TestInitialize]
        public void Initialize()
        {
            apiSettings = GetApiSettings();

            batchesApi = new BatchesApi(apiSettings.BaseUrl,
                apiSettings.Key,
                apiSettings.Secret);
        }

        [TestMethod]
        public void GetBatchShouldReturnValue()
        {
            var response = batchesApi.BatchesIdGet(1);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.GrossAmount > 0);
        }

        [TestMethod]
        public void GetInvalidBatchShouldReturn404()
        {
            try
            {
                batchesApi.BatchesIdGet(5);

                Assert.Fail();
            }
            catch (ApiException ex)
            {
                Assert.AreEqual(404, ex.ErrorCode);
            }
        }

        [TestMethod]
        public void GetBatchesShouldReturnCollection()
        {
            var response = batchesApi.BatchesGet();

            Assert.IsNotNull(response);
            Assert.IsTrue(response.TotalRecords > 0);
        }
    }
}