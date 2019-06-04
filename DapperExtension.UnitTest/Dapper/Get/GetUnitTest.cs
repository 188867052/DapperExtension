using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DapperExtension;
using DapperExtension.Entity;
using NUnit.Framework;

namespace DapperExtension.UnitTest.Dapper.Get
{
    /// <summary>
    /// Dapper unit test.
    /// </summary>
    [TestFixture]
    public class GetUnitTest
    {
        [Test]
        public async Task TestGet()
        {
            User user = DapperExtension.Connection.QueryFirst<User>();
            if (user != null)
            {
                var users = DapperExtension.Connection.Get<User>(1);
                Assert.IsNotNull(users);
                users = await DapperExtension.Connection.GetAsync<User>(new { id = 1 });
                Assert.IsNotNull(users);
            }
        }

        [Test]
        public void TestGetList()
        {
            User user = DapperExtension.Connection.QueryFirst<User>();
            if (user != null)
            {
                var users = DapperExtension.Connection.GetList<User>($"where login_name = '{user.LoginName}'");
                Assert.IsNotNull(users);
                users = DapperExtension.Connection.GetList<User>(new { login_name = "Strange 2" });
                Assert.IsNotNull(users);
            }
        }

        [Test]
        public void TestUpdateWithSpecifiedColumnName()
        {
            var log = DapperExtension.Connection.FindAll<Log>().FirstOrDefault();
            if (log != null)
            {
                int count = DapperExtension.Connection.Delete<Log>(log.Id);
                Assert.AreEqual(count, 1);
            }
        }

        [Test]
        public void TestFindAllByExpression()
        {
            using var dapper = DapperExtension.Connection;
            User user = DapperExtension.Connection.QueryFirst<User>();
            if (user != null)
            {
                var users = dapper.FindAll<User>(o => o.Id, user.Id);
                Assert.AreEqual(users.Count, 1);
                user = dapper.Find<User>(user.Id);
                Assert.IsNotNull(user);
                var roles = dapper.FindAll<Role>();
                Assert.IsNotNull(roles);
            }
        }

        [Test]
        public void TestGetListNullableWhere()
        {
            var users = DapperExtension.Connection.GetList<User>(new { avatar = DBNull.Value });
            Assert.IsNotNull(users);
        }

        [Test]
        public void TestGetListPaged()
        {
            var logs = DapperExtension.Connection.GetListPaged<Log>(2, 10, null, null);
            Assert.IsNotNull(logs);
            logs = DapperExtension.Connection.GetListPaged<Log>(1, 30, "where Id > @Id", null, new { Id = 14 });
            Assert.IsNotNull(logs);
        }

        [Test]
        public async Task TestGetListPagedAsync()
        {
            var logs = await DapperExtension.Connection.GetListPagedAsync<Log>(2, 10, null, null);
            Assert.IsNotNull(logs);
            logs = await DapperExtension.Connection.GetListPagedAsync<Log>(1, 30, "where Id > @Id", null, new { Id = 14 });
            Assert.IsNotNull(logs);
        }

        [Test]
        public void TestFilteredGetListParameters()
        {
            User user = DapperExtension.Connection.QueryFirst<User>();
            if (user != null)
            {
                IEnumerable<User> users = DapperExtension.Connection.GetList<User>("where Id = @Id", new { user.Id });
                Assert.IsNotNull(users);
                users = DapperExtension.Connection.GetList<User>(new { user.Id });
                Assert.IsNotNull(users);
            }
        }

        [Test]
        public void TestGetListWithNullWhere()
        {
            var users = DapperExtension.Connection.GetList<User>(null);
            Assert.IsNotNull(users);
            users = DapperExtension.Connection.GetList<User>(new { });
            Assert.IsNotNull(users);
            users = DapperExtension.Connection.GetList<User>();
            Assert.IsNotNull(users);
        }

        [Test]
        public async Task TestGetListWithNullWhereAsync()
        {
            var users = await DapperExtension.Connection.GetListAsync<User>(null);
            Assert.IsNotNull(users);
            users = await DapperExtension.Connection.GetListAsync<User>(new { });
            Assert.IsNotNull(users);
            users = await DapperExtension.Connection.GetListAsync<User>();
            Assert.IsNotNull(users);
        }
    }
}
