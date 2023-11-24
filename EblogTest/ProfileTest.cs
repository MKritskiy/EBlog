using EblogTest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EblogTest
{
    internal class ProfileTest : Helpers.BaseTest
    {
        [Test]
        public async Task AddTest()
        {
            using(TransactionScope scope = Helper.CreateTransactionScope()) 
            {
                await profile.AddOrUpdate(
                    new EBlog.DAL.Models.ProfileModel()
                    {
                        UserId = 19,
                        FirstName = "Иван",
                        LastName = "Иванов",
                        ProfileName = "Тест"
                    });
                var result = await profile.Get(19);
                Assert.That(result, Is.Not.Null);

                Assert.That(result.FirstName, Is.EqualTo("Иван"));
                Assert.That(result.LastName, Is.EqualTo("Иванов"));
                Assert.That(result.ProfileName, Is.EqualTo("Тест"));
                Assert.That(result.UserId, Is.EqualTo(19));

            }
        }

        [Test]
        public async Task UpdateTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                var profileModel = new EBlog.DAL.Models.ProfileModel()
                    {
                        UserId = 19,
                        FirstName = "Иван",
                        LastName = "Иванов",
                        ProfileName = "Тест"
                    };

                await profile.AddOrUpdate(profileModel);

                profileModel.FirstName = "Иван1";

                await profile.AddOrUpdate(profileModel);



                var result = await profile.Get(19);
                Assert.That(result, Is.Not.Null);

                Assert.That(result.FirstName, Is.EqualTo("Иван1"));
                Assert.That(result.UserId, Is.EqualTo(19));

            }
        }
    }
}
