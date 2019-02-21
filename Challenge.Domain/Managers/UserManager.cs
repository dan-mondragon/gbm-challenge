using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Challenge.Domain.Entities;
using Challenge.Domain.Exceptions;
using Challenge.Domain.Repositories;
using Challenge.Domain.Utils;
using Challenge.Domain.ViewModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Challenge.Domain.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _repository;
        private readonly AppSettings _appSettings;

        public UserManager(IUserRepository repository, IOptions<AppSettings> appSettings)
        {
            _repository = repository;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<UserViewModel> AddUserAsync(UserRequestViewModel userViewModel, CancellationToken ct = default(CancellationToken))
        {
            if (string.IsNullOrWhiteSpace(userViewModel.Password))
                throw new AppException("Password is required");

            var user = await _repository.GetUserAsync(userViewModel.Username);
            if (user != null)
                throw new AppException("Username \"" + user.Username + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(userViewModel.Password, out passwordHash, out passwordSalt);

            user = new User
            {
                Username = userViewModel.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                CreatedAt = DateTime.Now
            };

            user = await _repository.AddAsync(user, ct);
            return new UserViewModel() { UserId = user.UserId };
        }

        /// <summary>
        /// Check if user exists and is valid, and generates a JSON Web Token (jwt).
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<UserViewModel> Authenticate(string username, string password, CancellationToken ct = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = await _repository.GetUserAsync(username, ct);
            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new UserViewModel() { Username = user.Username, Token = tokenHandler.WriteToken(token) };
        }

        /// <summary>
        /// Check if password is correct.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="storedHash"></param>
        /// <param name="storedSalt"></param>
        /// <returns></returns>
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Generates the password hash.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
