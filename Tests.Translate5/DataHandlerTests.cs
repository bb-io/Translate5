using Apps.Translate5.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Translate5Tests.Base;

namespace Tests.Translate5
{
    [TestClass]
    public class DataHandlerTests : TestBase
    {
        [TestMethod]
        public async Task TaskDataHandler_IsSuccess()
        {
            var handler = new TaskDataHandler(InvocationContext);
            var context = new DataSourceContext
            {
                SearchString = ""
            };
            var result = await handler.GetDataAsync(context, CancellationToken.None);

            foreach (var item in result)
            {
                Console.WriteLine($"Key: {item.Key}, Value: {item.Value}");
            }
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task TaskGuidDataHandler_IsSuccess()
        {
            var handler = new TaskGuidDataHandler(InvocationContext);
            var context = new DataSourceContext
            {
                SearchString = ""
            };
            var result = await handler.GetDataAsync(context, CancellationToken.None);

            foreach (var item in result)
            {
                Console.WriteLine($"Key: {item.Key}, Value: {item.Value}");
            }
            Assert.IsNotNull(result);
        }
    }
}
