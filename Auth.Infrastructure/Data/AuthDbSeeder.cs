using Auth.Domain.Entities;
using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Data
{
    public static class AuthDbSeeder
    {
        public static void Seed(AuthDbContext context)
        {
            var admin = context.Users.FirstOrDefault(x => x.Email == "admin@system.com");
            if (admin == null)
            {
                context.Users.Add(new AppUser
                {
                    FullName = "Admin",
                    Email = "admin@system.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                    Role = "Admin"
                });

                context.SaveChanges();
            }
        }
    }
}
