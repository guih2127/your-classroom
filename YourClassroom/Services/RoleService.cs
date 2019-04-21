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
            using (var context = new YourClassroomEntities())
            {
                var userRoles = context.AspNetRoles.Include(r => r.AspNetUsers).ToList();

                var userRole = (from r in userRoles
                                from u in r.AspNetUsers
                                where u.Id == UserID
                                select r.Name).First();

                return userRole;
            }
        }
    }
}