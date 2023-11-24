﻿namespace EBlog.BL.Auth
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IDbSession dbSession;
        public CurrentUser(IHttpContextAccessor httpContextAccessor, IDbSession dbSession)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.dbSession = dbSession;
        }
        public async Task<bool> IsLoggedInAsync()
        {
            return await dbSession.IsLoggedIn();
        }
    }
}