using EblogTest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EblogTest
{
    internal class SessionTest : Helpers.BaseTest
    {
        [Test]
        [NonParallelizable]
        public async Task CreateSessionTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                ((TestCookie)this.webCookie).Clear();
                this.dbSession.ResetSessionCache();
                var session = await this.dbSession.GetSession();

                var dbSession = await this.dbSessionDAL.Get(session.DbSessionId);

                Assert.NotNull(dbSession, "Session should not be null");

                Assert.That(dbSession.DbSessionId, Is.EqualTo(session.DbSessionId));

                var session2 = await this.dbSession.GetSession();
                Assert.That(dbSession.DbSessionId, Is.EqualTo(session2.DbSessionId));
            }
        }

        [Test]
        [NonParallelizable]
        public async Task CreateAuthorizedSessionTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                ((TestCookie)this.webCookie).Clear();
                this.dbSession.ResetSessionCache();
                var session = await this.dbSession.GetSession();
                await this.dbSession.SetUserId(10);


                var dbSession = await this.dbSessionDAL.Get(session.DbSessionId);

                Assert.NotNull(dbSession, "Session should not be null");

                Assert.That(dbSession.UserId, Is.EqualTo(10));

                var session2 = await this.dbSession.GetSession();
                Assert.That(dbSession.DbSessionId, Is.EqualTo(session2.DbSessionId));

                int? userid = await this.currentUser.GetCurrentUserId();
                Assert.That(userid, Is.EqualTo(10));
            }
        }
    }
}
