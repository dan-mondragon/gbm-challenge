using Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.Data.Configuration
{
    public class UserConfiguration
    {
        /// <summary>
        /// User Entity Configuration
        /// </summary>
        /// <param name="entity"></param>
        public UserConfiguration(EntityTypeBuilder<User> entity)
        {
            entity.Property(e => e.UserId).HasMaxLength(10).IsRequired();
            entity.HasMany(e => e.Vehicles).WithOne(e => e.User).IsRequired().OnDelete(DeleteBehavior.Restrict);


            byte[] passwordHash;
            byte[] passwordSalt;
            CreatePasswordHash("Abc_123", out passwordHash, out passwordSalt);
            entity.HasData(
                new User() { UserId = 1, Username = "Admin", PasswordHash = passwordHash, PasswordSalt = passwordSalt }
            );
        }

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
