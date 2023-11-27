﻿using EBlog.DAL.Models;

namespace EBlog.DAL
{
    public interface IBlogDAL
    {
        public Task<BlogModel?> Get(int blogId);
        public Task<IEnumerable<BlogModel>> GetByUserId(int userId);
        public Task<IEnumerable<BlogModel>> Search(int count);
        public Task<int?> Add(BlogModel model);
        public Task Update(BlogModel model);

    }
}
