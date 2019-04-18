using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YourClassroom.Models;

namespace YourClassroom.Services
{
    public class UserService
    {
        ApplicationDbContext context;

        public UserService()
        {
            context = new ApplicationDbContext();
        }

        public ApplicationUser GetUserById(string UserId)
        {
            ApplicationUser user = context.Users.First(u => u.Id == UserId);
            return user;
        }
    }
}