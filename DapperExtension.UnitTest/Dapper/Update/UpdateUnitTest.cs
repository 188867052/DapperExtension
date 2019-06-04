using System;
using DapperExtension;
using NUnit.Framework;

namespace Core.UnitTest.Dapper.Update
{
    /// <summary>
    /// Api unit test.
    /// </summary>
    [TestFixture]
    public class UpdateUnitTest
    {
        [Test]
        public void TestUpdate()
        {
            var message = Guid.NewGuid().ToString();
            var log = DapperExtension.DapperExtension.Connection.QueryFirst<Log>();
            log.Message = message;
            DapperExtension.DapperExtension.Connection.Update(log);
            var logFind = DapperExtension.DapperExtension.Connection.Find<Log>(log.Id);

            Assert.AreEqual(logFind.Message, message);
        }

        [Test]
        public void TestMassUpdate()
        {
            var log = DapperExtension.DapperExtension.Connection.QueryFirst<Log>();
            if (log != null)
            {
                int count = 0;
                using (var transaction = DapperExtension.DapperExtension.BeginTransaction())
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        log.Message = Guid.NewGuid().ToString();
                        count += DapperExtension.DapperExtension.Connection.Update(log, transaction);
                    }

                    transaction.Commit();
                }

                Assert.AreEqual(count, 1000);
            }
        }
    }
}