﻿using EBlog.BL.Auth;
using EblogTest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EblogTest
{
    internal class CurrentUserTest : BaseTest
    {
        [Test]
        public async Task BaseRegistrationTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                await CreateAndAuthUser();

                bool isLoggedIn = await this.currentUser.IsLoggedInAsync();
                Assert.True(isLoggedIn);

                webCookie.Delete(AuthConstants.SessionCookieName);
                dbSession.ResetSessionCache();

                isLoggedIn = await this.currentUser.IsLoggedInAsync();
                Assert.True(isLoggedIn);

                webCookie.Delete(AuthConstants.SessionCookieName);
                webCookie.Delete(AuthConstants.RememberMeCookieName);
                dbSession.ResetSessionCache();

                isLoggedIn = await this.currentUser.IsLoggedInAsync();
                Assert.False(isLoggedIn);
            }
        }

        public async Task<int> CreateAndAuthUser()
        {
            string email = Guid.NewGuid().ToString() + "@test.com";

            int userId = await authBL.CreateUser(
                new EBlog.DAL.Models.UserModel()
                {
                    Email = email,
                    Password = "qwer1234"
                });
            return await authBL.Authenticate(email, "qwer1234", true);
        }
    }
}
