using Auth.Application.Commands;
using Auth.Domain.Entities;
using Auth.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Handlers
{
    public class RegisterHandler
    {
        private readonly UserRepository _repo;

        public RegisterHandler(UserRepository repo)
        {
            _repo = repo;
        }

        public async Task Handle(RegisterCommandd cmd)
        {
            var user = new AppUser
            {
                FullName = cmd.FullName,
                Email = cmd.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(cmd.Password),
                Role = "User"
            };

            await _repo.AddAsync(user);
        }
    }
}
