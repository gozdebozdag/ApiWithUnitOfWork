using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWithUnitOfWork.Domain.Repository
{
    public interface IUnitOfWork:IDisposable
    {
        IUserRepository User {  get; }
        Task<int> CompleteAsync(); // Asenkron metodun tanımlandığından emin olun
        int Save();
    }
}
