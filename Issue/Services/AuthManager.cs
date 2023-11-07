using AutoMapper;
using Issues.Models;
using Issues.Services.Contracts;
using Microsoft.AspNetCore.Identity;

namespace Issues.Services
{
    public class AuthManager : IAuthService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        public AuthManager(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public IEnumerable<IdentityRole> Roles => 
            _roleManager.Roles;

        public async Task<IdentityResult> CreateUser(UserForCreation userDt)
        {
            var user = _mapper.Map<IdentityUser>(userDt);
            var result = await _userManager.CreateAsync(user, userDt.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors;
                throw new Exception("User could not be created. Errors: " + string.Join(", ", errors.Select(e => e.Description)));
            }
            if (userDt.Roles.Count > 0) 
            {
                var roleResult = await _userManager.AddToRolesAsync(user, userDt.Roles);
                if(!roleResult.Succeeded) 
                {
                    throw new Exception("System have problems roles.");
                }
            }
            return result;
        }

        public async Task<IdentityResult> DeleteOneUser(string userName)
        {
            var user = await GetOneUser(userName);
            return await _userManager.DeleteAsync(user);
        }

        public IEnumerable<IdentityUser> GetAllUsers()
        {
            return _userManager.Users.ToList();
        }

        public async Task<IdentityUser> GetOneUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user is not null)
            {
                return user;
            }
            throw new Exception("User could not be found");

        }

        public async Task<UserForUpdate> GetOneUserForUpdate(string userName)
        {
            var user = await GetOneUser(userName);
                var userDt = _mapper.Map<UserForUpdate>(user);
                userDt.Roles = new HashSet<string>(Roles.Select(r  => r.Name).ToList());
                userDt.UserRoles = new HashSet<string>(await _userManager.GetRolesAsync(user));
                return userDt;
        }

        public async Task<IdentityResult> ResetPassword(ResetPasswordDt model)
        {
            var user = await GetOneUser(model.UserName);
            await _userManager.RemovePasswordAsync(user);
            var result = await _userManager.AddPasswordAsync(user, model.Password);
            return result;
        }

        public async Task Update(UserForUpdate userDt)
        {
            var user = await GetOneUser(userDt.UserName);
            user.Email = userDt.Email;

            var result = await _userManager.UpdateAsync(user);
            if (userDt.Roles.Count > 0)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var r1 = await _userManager.RemoveFromRolesAsync(user, userRoles);
                var r2 = await _userManager.AddToRolesAsync(user, userDt.Roles);
            }
            return;
        }
    }
}
