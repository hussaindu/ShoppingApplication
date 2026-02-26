using Auth.Domain.Entities;
using Auth.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Repository
{
    public interface IUserRepository
    {
        public Task AddAsync(AppUser user);
        public Task<AppUser?> GetByEmailAsync(string email);
        public  Task<List<AppUser>> GetAllAsync();
        public  Task<AppUser?> GetByIdAsync(int id);

        public  Task UpdateAsync(AppUser user);

        public  Task DeleteAsync(AppUser user);
    }
}
