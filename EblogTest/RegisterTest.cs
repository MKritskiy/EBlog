using EBlog.BL.Auth;
using EBlog.BL.Exeption;
using EBlog.DAL;
using EblogTest.Helpers;
using System.Transactions;

namespace EblogTest
{
    public class RegisterTests : Helpers.BaseTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task BaseRegistrationTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                string email = Guid.NewGuid().ToString() + "@test.com";
                //validate: should not be in the DB
               authBL.ValidateEmail(email).GetAwaiter().GetResult();
                

                int userId = await authBL.CreateUser(
                    new EBlog.DAL.Models.UserModel()
                    {
                        Email = email,
                        Password = "qwer1234"
                    });

                Assert.Greater(userId, 0);
                var userdalresult = await authDAL.GetUser(userId);
                Assert.That(email, Is.EqualTo(userdalresult.Email));
                Assert.NotNull(userdalresult.Salt);

                var userbyemaildalresult = await authDAL.GetUser(email);
                Assert.That(email, Is.EqualTo(userbyemaildalresult.Email));
                //validate: should be in the DB
                Assert.Throws<DuplicateEmailException>(delegate
                {
                    authBL.ValidateEmail(email).GetAwaiter().GetResult();
                });

                string encpassword = encrypt.HashPassword("qwer1234", userbyemaildalresult.Salt);
                Assert.That(encpassword, Is.EqualTo(userbyemaildalresult.Password));


            }
        }
    }
}