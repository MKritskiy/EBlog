using EBlog.BL.Blog;
using EblogTest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EblogTest
{
    internal class CommentTest : Helpers.BaseTest
    {
        [Test]
        public async Task AddTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {

                await profile.AddOrUpdate(
                    new EBlog.DAL.Models.ProfileModel()
                    {
                        UserId = 19,
                        FirstName = "Иван",
                        LastName = "Иванов",
                        ProfileName = "Тест"
                    });
                var curprofile = await profile.Get(19);
                Assert.That(curprofile, Is.Not.Null);

                await comment.AddOrUpdate(
                    new EBlog.DAL.Models.CommentModel()
                    {
                        CommentContent = "Содержимое",
                        CommentHeader = "Заголовок",
                        ProfileId = curprofile.ProfileId ?? 0,
                        BlogId= 19,
                        
                    });
                var result = await comment.GetByBlogId(19);
                Assert.That(result?.FirstOrDefault(), Is.Not.Null);

                Assert.That(result?.FirstOrDefault()?.CommentContent, Is.EqualTo("Содержимое"));
                Assert.That(result?.FirstOrDefault()?.CommentHeader, Is.EqualTo("Заголовок"));
                Assert.That(result?.FirstOrDefault()?.BlogId, Is.EqualTo(19));
                Assert.That(result?.FirstOrDefault()?.ProfileId, Is.EqualTo(curprofile.ProfileId));
                Assert.That(result?.FirstOrDefault()?.Profile, Is.Not.Null);



            }
        }

        [Test]
        public async Task UpdateTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {

                await profile.AddOrUpdate(
                    new EBlog.DAL.Models.ProfileModel()
                    {
                        UserId = 19,
                        FirstName = "Иван",
                        LastName = "Иванов",
                        ProfileName = "Тест"
                    });
                var curprofile = await profile.Get(19);
                Assert.That(curprofile, Is.Not.Null);

                var commentModel = new EBlog.DAL.Models.CommentModel()
                {
                    CommentContent = "Содержимое",
                    CommentHeader = "Заголовок",
                    ProfileId = curprofile.ProfileId ?? 0,
                    BlogId = 19,

                };

                await comment.AddOrUpdate(commentModel);

                commentModel.CommentHeader = "Заголовок1";

                await comment.AddOrUpdate(commentModel);



                var result = await comment.GetByBlogId(19);
                Assert.That(result?.FirstOrDefault(), Is.Not.Null);

                Assert.That(result?.FirstOrDefault()?.CommentHeader, Is.EqualTo("Заголовок1"));
                Assert.That(result?.FirstOrDefault()?.BlogId, Is.EqualTo(19));
                Assert.That(result?.FirstOrDefault()?.ProfileId, Is.EqualTo(curprofile.ProfileId));
                Assert.That(result?.FirstOrDefault()?.Profile, Is.Not.Null);


            }
        }
    }
}
