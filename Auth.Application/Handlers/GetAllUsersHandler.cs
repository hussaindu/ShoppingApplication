using Auth.Domain.Entities;
using Auth.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Handlers
{
    public class GetAllUsersHandler
    {

        private readonly UserRepository _repo;

        public GetAllUsersHandler(UserRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<AppUser>> Handle()
            => await _repo.GetAllAsync();
    }
}
