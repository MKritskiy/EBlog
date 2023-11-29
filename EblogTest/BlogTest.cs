using EBlog.BL.Profile;
using EblogTest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EblogTest
{
    internal class BlogTest : Helpers.BaseTest
    {
        [Test]
        public async Task AddTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                await blog.AddOrUpdate(
                    new EBlog.DAL.Models.BlogModel()
                    { 
                        BlogContent = "Содержимое",
                        BlogHeader = "Заголовок",
                        ProfileId = 19,
                    });
                var result = await blog.GetByProfileId(19);
                Assert.That(result?.FirstOrDefault(), Is.Not.Null);

                Assert.That(result?.FirstOrDefault()?.BlogContent, Is.EqualTo("Содержимое"));
                Assert.That(result?.FirstOrDefault()?.BlogHeader, Is.EqualTo("Заголовок"));
                Assert.That(result?.FirstOrDefault()?.ProfileId, Is.EqualTo(19));

            }
        }

        [Test]
        public async Task UpdateTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                var blogModel = new EBlog.DAL.Models.BlogModel()
                {
                    BlogContent = "Содержимое",
                    BlogHeader = "Заголовок",
                    ProfileId = 19,

                };

                await blog.AddOrUpdate(blogModel);

                blogModel.BlogHeader = "Заголовок1";

                await blog.AddOrUpdate(blogModel);



                var result = await blog.GetByProfileId(19);
                Assert.That(result?.FirstOrDefault(), Is.Not.Null);

                Assert.That(result?.FirstOrDefault()?.BlogHeader, Is.EqualTo("Заголовок1"));
                Assert.That(result?.FirstOrDefault()?.ProfileId, Is.EqualTo(19));

            }
        }

    }
}
