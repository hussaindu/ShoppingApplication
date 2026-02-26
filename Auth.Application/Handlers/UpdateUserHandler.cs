using Auth.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Handlers
{
    public class UpdateUserHandler
    {
        private readonly UserRepository _repo;

        public UpdateUserHandler(UserRepository repo)
        {
            _repo = repo;
        }

        public async Task Handle(int id, string role)
        {
            var user = await _repo.GetByIdAsync(id)
                ?? throw new Exception("User not found");

            user.Role = role;
            await _repo.UpdateAsync(user);
        }
    }
}
