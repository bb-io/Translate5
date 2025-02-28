using Apps.Translate5.Actions;
using Apps.Translate5.Models.Request.Tasks;
using Translate5Tests.Base;

namespace Tests.Translate5
{
    [TestClass]
    public class TaskTests : TestBase
    {
        [TestMethod]
        public async Task ExportTranslatedFileReturnsValues()
        {
            var action = new TaskActions(InvocationContext,FileManager);
            var taskRequest = new TaskRequest { TaskId = "99443" };

            var response = await action.ExportTaskFile(taskRequest);

            Assert.IsNotNull(response);
        }

    }
}
