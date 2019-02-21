using Challenge.Domain.ViewModels;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Domain.Managers
{
    public interface IUserManager
    {
        Task<UserViewModel> Authenticate(string username, string password, CancellationToken ct = default(CancellationToken));
        Task<UserViewModel> AddUserAsync(UserRequestViewModel userViewModel, CancellationToken ct = default(CancellationToken));
    }
}
