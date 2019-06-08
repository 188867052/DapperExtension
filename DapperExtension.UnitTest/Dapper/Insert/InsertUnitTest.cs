using System;
using System.Globalization;
using System.Threading.Tasks;
using DapperExtension.Entity;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace DapperExtension.UnitTest.Dapper.Insert
{
    /// <summary>
    /// Api unit test.
    /// </summary>
    [TestFixture]
    public class InsertUnitTest
    {
        [Test]
        public void TestInsertWithSpecifiedPrimaryKey()
        {
            var log = new Log { LogLevel = (int)LogLevel.Information, CreateTime = DateTime.Now, Message = "TestInsertWithSpecifiedPrimaryKey" };
            var id = DapperExtension.Connection.InsertReturnKey(log);
            Assert.Greater(id, 0);
        }

        [Test]
        public async Task TestInsertAsyncWithSpecifiedPrimaryKeyAsync()
        {
            var log = new Log { LogLevel = (int)LogLevel.Information, CreateTime = DateTime.Now, Message = "TestInsertWithSpecifiedPrimaryKey" };
            var idTask = DapperExtension.Connection.InsertAsync(log);
            var id = await idTask.ConfigureAwait(false);
            Assert.Greater(id, 0);
        }

        [Test]
        public void TestInsertWithMultiplePrimaryKeys()
        {
            var keyMaster = new MultiplePrimaryKeyTable { Id = Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture), Name = Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture) };
            string id = DapperExtension.Connection.InsertReturnKey(keyMaster);
            Assert.IsNotNull(id);
        }

        [Test]
        public async Task TestInsertAsyncWithMultiplePrimaryKeysAsync()
        {
            Task<dynamic> task = DapperExtension.Connection.InsertAsync(new MultiplePrimaryKeyTable { Id = Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture), Name = Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture) });
            var result = await task.ConfigureAwait(false);
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestInsertUsingGenericLimitedFields()
        {
            var log = new Log { LogLevel = (int)LogLevel.Information, Message = "TestInsertWithSpecifiedPrimaryKey" };
            var id = DapperExtension.Connection.InsertReturnKey(log);
            Assert.Greater(id, 0);
        }

        [Test]
        public async Task TestInsertAsyncUsingGenericLimitedFieldsAsync()
        {
            var log = new Log { LogLevel = (int)LogLevel.Information, Message = "TestInsertWithSpecifiedPrimaryKey" };
            dynamic task = await DapperExtension.Connection.InsertAsync(log).ConfigureAwait(false);
            Assert.IsNotNull(task);
        }

        [Test]
        public void TestMassInsert()
        {
            int count = 0;
            using (var transaction = DapperExtension.BeginTransaction())
            {
                for (int i = 0; i < 2; i++)
                {
                    Log log = new Log { Message = Guid.NewGuid().ToString(), LogLevel = (int)LogLevel.None };
                    count += DapperExtension.Connection.Insert(log, transaction);
                }

                transaction.Commit();
            }

            Assert.AreEqual(count, 2);
        }
    }
}