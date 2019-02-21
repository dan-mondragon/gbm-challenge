using Challenge.Domain.Entities;
using Challenge.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ChallengeContext _context;

        public UserRepository(ChallengeContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        /// <summary>
        /// Find the user by username and password.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<User> GetUserAsync(string username, CancellationToken ct = default(CancellationToken))
        {
            return await _context.User.SingleOrDefaultAsync(x => x.Username == username, ct);
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="newVehicle"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<User> AddAsync(User newUser, CancellationToken ct = default(CancellationToken))
        {
            _context.User.Add(newUser);
            await _context.SaveChangesAsync(ct);
            return newUser;
        }
    }
}
