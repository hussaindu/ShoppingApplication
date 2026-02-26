using Auth.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Handlers
{
    public class DeleteUserHandler
    {
        private readonly UserRepository _repo;

        public DeleteUserHandler(UserRepository repo)
        {
            _repo = repo;
        }

        public async Task Handle(int id)
        {
            var user = await _repo.GetByIdAsync(id)
                ?? throw new Exception("User not found");

            await _repo.DeleteAsync(user);
        }
    }
}
