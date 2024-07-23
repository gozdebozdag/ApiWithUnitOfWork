using ApiWithUnitOfWork.DataAccess.Context;
using ApiWithUnitOfWork.Domain.Repository;
using System;
using System.Threading.Tasks;

namespace ApiWithUnitOfWork.DataAccess.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApiDbContext _context;
        private IUserRepository _userRepository;

        public UnitOfWork(ApiDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public IUserRepository User => _userRepository;

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
