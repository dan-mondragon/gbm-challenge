using Challenge.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Domain.Repositories
{
    public interface IUserRepository : IDisposable
    {
        Task<User> GetUserAsync(string username, CancellationToken ct = default(CancellationToken));
        Task<User> AddAsync(User newUser, CancellationToken ct = default(CancellationToken));
    }
}
