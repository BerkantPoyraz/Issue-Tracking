using Issues.Models;
using Microsoft.AspNetCore.Identity;


namespace Issues.Services.Contracts
{
    public interface IAuthService
    {
        IEnumerable<IdentityRole> Roles { get; }

        IEnumerable<IdentityUser> GetAllUsers();
        Task<IdentityUser> GetOneUser(string userName);
        Task<UserForUpdate> GetOneUserForUpdate(string userName);
        Task<IdentityResult> CreateUser(UserForCreation userDt);
        Task Update(UserForUpdate userDt);
        Task<IdentityResult> ResetPassword (ResetPasswordDt model);
        Task<IdentityResult> DeleteOneUser(string userName);
    }
}
