using EBlog.BL.Auth;
using EBlog.BL.Exeption;
using EBlog.DAL.Models;
using EblogTest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EblogTest
{
    internal class AuthTest : Helpers.BaseTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task BaseAuthTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                string email = Guid.NewGuid().ToString() + "@test.com";
                //validate: should not be in the DB
                await authBL.ValidateEmail(email);
                

                int userId = await authBL.CreateUser(
                    new EBlog.DAL.Models.UserModel()
                    {
                        Email = email,
                        Password = "qwer1234"
                    });
                Assert.Throws<AuthorizationException>(delegate { 
                    authBL.Authenticate("sfer", "111", false).GetAwaiter().GetResult(); 
                });

                Assert.Throws<AuthorizationException>(delegate {
                    authBL.Authenticate(email, "111", false).GetAwaiter().GetResult();
                });

                Assert.Throws<AuthorizationException>(delegate {
                    authBL.Authenticate("wewrs", "qwer1234", false)
                    .GetAwaiter().GetResult();
                });

                await authBL.Authenticate(email, "qwer1234", false);


                string? authCookie = this.webCookie.Get(AuthConstants.SessionCookieName);
                Assert.NotNull(authCookie);

                string? rememberMeCookie = this.webCookie.Get(AuthConstants.RememberMeCookieName);
                Assert.Null(rememberMeCookie);
            }

        }
        [Test]
        public async Task RememberMeTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                string email = Guid.NewGuid().ToString() + "@test.com";

                // create user
                int userId = await authBL.CreateUser(
                    new UserModel()
                    {
                        Email = email,
                        Password = "qwer1234"
                    });

                await authBL.Authenticate(email, "qwer1234", true);

                string? authCookie = this.webCookie.Get(AuthConstants.SessionCookieName);
                Assert.NotNull(authCookie);

                string? rememberMeCookie = this.webCookie.Get(AuthConstants.RememberMeCookieName);
                Assert.NotNull(rememberMeCookie);
            }
        }
    }
}
