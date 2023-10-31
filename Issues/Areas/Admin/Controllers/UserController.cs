using Issues.Models;
using Issues.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Issues.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class UserController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly UserManager<IdentityUser> _userManager;
        public UserController(IServiceManager manager, UserManager<IdentityUser> userManager)
        {
            _manager = manager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var users = _manager.AuthService.GetAllUsers();
            return View(users);
        }
        public IActionResult Create() 
        {
            return View(new UserForCreation()
            {
                Roles = new HashSet<string>(_manager
                .AuthService
                .Roles
                .Select (r => r.Name)
                .ToList())
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] UserForCreation userdes)
        {
            var result = await _manager.AuthService.CreateUser(userdes);
            return result.Succeeded ? RedirectToAction("Index") : View();
        }

        public async Task<IActionResult> Update([FromRoute(Name = "id")]string id)
        {
            var user = await _manager.AuthService.GetOneUserForUpdate(id);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm] UserForUpdate userDt)
        {
            if (ModelState.IsValid)
            {
                await _manager.AuthService.Update(userDt);
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> ResetPassword([FromRoute(Name = "id")] string id)
        {
            return View(new ResetPasswordDt()
            {
                UserName = id
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordDt model)
        {
            var result = await _manager.AuthService.ResetPassword(model);
            return result.Succeeded ? RedirectToAction("Index") : View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteOneUser([FromForm]UserDt userDt)
        {
            var result = await _manager.AuthService.DeleteOneUser(userDt.UserName);

            return result.Succeeded ? RedirectToAction("Index") : View();
        }
    }
}
