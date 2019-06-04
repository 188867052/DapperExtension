using System.Threading.Tasks;
using DapperExtension.Entity;
using NUnit.Framework;

namespace DapperExtension.UnitTest.Dapper.Count
{
    /// <summary>
    /// Dapper extension unit test.
    /// </summary>
    [TestFixture]
    public class CountUnitTest
    {
        [Test]
        public async Task TestRecordCountAsync()
        {
            User user = DapperExtension.Connection.QueryFirst<User>();
            if (user != null)
            {
                var count = DapperExtension.Connection.RecordCount<User>($"where Id = '{user.Id}'");
                Assert.AreEqual(count, 1);
                count = DapperExtension.Connection.RecordCount<User>();
                Assert.GreaterOrEqual(count, 0);
                count = await DapperExtension.Connection.RecordCountAsync<User>().ConfigureAwait(false);
                Assert.GreaterOrEqual(count, 0);
            }
        }

        [Test]
        public async Task TestRecordCount()
        {
            User user = DapperExtension.Connection.QueryFirst<User>();
            if (user != null)
            {
                int count = await DapperExtension.Connection.RecordCountAsync<User>().ConfigureAwait(false);
                Assert.GreaterOrEqual(count, 0);
            }
        }

        [TestCase(1)]
        public async Task TestRecordCountByObjectAsync(int id)
        {
            User user = DapperExtension.Connection.QueryFirst<User>();
            if (user != null)
            {
                int count = await DapperExtension.Connection.RecordCountAsync<User>(new { id }).ConfigureAwait(true);
                Assert.GreaterOrEqual(count, 0);
                count = DapperExtension.Connection.RecordCount<User>(new { id });
                Assert.GreaterOrEqual(count, 0);
            }
        }

        [Test]
        public async Task TestRecordCountByObjectAsyncIgnoreCaseAsync()
        {
            User user = DapperExtension.Connection.QueryFirst<User>();
            if (user != null)
            {
                int count = await DapperExtension.Connection.RecordCountAsync<User>(new { ID = 10 }).ConfigureAwait(false);
                Assert.GreaterOrEqual(count, 0);
            }
        }
    }
}
