using ApiWithUnitOfWork.DataAccess.Context;
using ApiWithUnitOfWork.Domain.Entities;
using ApiWithUnitOfWork.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWithUnitOfWork.DataAccess.Implemenatation
{
    public class UserRepository :GenericRepository<User>,IUserRepository
    {
        public UserRepository(ApiDbContext context) : base(context)
        {
        }

        public IEnumerable<User> GetActiveUsers()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetActiveUsersAsync()
        {
            throw new NotImplementedException();
        }


        // Email ile kullanıcı arama
        public User GetByEmail(string email)
        {
            return _context.Set<User>().FirstOrDefault(x => x.Email == email);
        }

       

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}

