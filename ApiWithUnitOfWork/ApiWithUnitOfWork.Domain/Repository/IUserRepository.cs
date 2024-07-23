using ApiWithUnitOfWork.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWithUnitOfWork.Domain.Repository
{
    public interface IUserRepository:IGenericRepository<User>
    {
        IEnumerable<User> GetActiveUsers();
        User GetByEmail(string email);
        Task<IEnumerable<User>> GetActiveUsersAsync();
        Task<User> GetByEmailAsync(string email);
    }
}
