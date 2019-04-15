using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using YourClassroom.Models;

namespace YourClassroom.Services
{
    public class RoleService
    {
        ApplicationDbContext context;

        public RoleService()
        {
            context = new ApplicationDbContext();
        }
        public string GetUserRoleById(string UserID)
        {
            var userRoles = context.Roles.Include(r => r.Users).ToList();

            var userRole = (from r in userRoles
                            from u in r.Users
                            where u.UserId == UserID
                            select r.Name).First();

            return userRole;
        }
    }
}