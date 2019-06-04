using System.Threading.Tasks;
using DapperExtension;
using NUnit.Framework;

namespace Core.UnitTest.Dapper.Count
{
    /// <summary>
    /// Api unit test.
    /// </summary>
    [TestFixture]
    public class CountUnitTest
    {
        [Test]
        public async Task TestRecordCountAsync()
        {
            User user = DapperExtension.DapperExtension.Connection.QueryFirst<User>();
            if (user != null)
            {
                var count = DapperExtension.DapperExtension.Connection.RecordCount<User>($"where Id = '{user.Id}'");
                Assert.AreEqual(count, 1);
                count = DapperExtension.DapperExtension.Connection.RecordCount<User>();
                Assert.GreaterOrEqual(count, 0);
                count = await DapperExtension.DapperExtension.Connection.RecordCountAsync<User>();
                Assert.GreaterOrEqual(count, 0);
            }
        }

        [Test]
        public async Task TestRecordCount()
        {
            User user = DapperExtension.DapperExtension.Connection.QueryFirst<User>();
            if (user != null)
            {
                int count = await DapperExtension.DapperExtension.Connection.RecordCountAsync<User>();
                Assert.GreaterOrEqual(count, 0);
            }
        }

        [TestCase(1)]
        public async Task TestRecordCountByObjectAsync(int id)
        {
            User user = DapperExtension.DapperExtension.Connection.QueryFirst<User>();
            if (user != null)
            {
                int count = await DapperExtension.DapperExtension.Connection.RecordCountAsync<User>(new { id });
                Assert.GreaterOrEqual(count, 0);
                count = DapperExtension.DapperExtension.Connection.RecordCount<User>(new { id });
                Assert.GreaterOrEqual(count, 0);
            }
        }

        [Test]
        public async Task TestRecordCountByObjectAsyncIgnoreCaseAsync()
        {
            User user = DapperExtension.DapperExtension.Connection.QueryFirst<User>();
            if (user != null)
            {
                int count = await DapperExtension.DapperExtension.Connection.RecordCountAsync<User>(new { ID = 10 });
                Assert.GreaterOrEqual(count, 0);
            }
        }
    }
}
