using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Issues.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Issue.ViewModel;

namespace Issues.Controllers
{
    [Authorize]
    public class BugController : Controller
    {
        private readonly IssueTrackingContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BugController(IssueTrackingContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            return View(_context.Bugs.ToList());
        }

        public IActionResult Create()
        {
            var projects = _context.Projects.Select(project => new SelectListItem
            {
                Value = project.ProjectId.ToString(),
                Text = project.ProjectName
            }).ToList();

            var bugCreateViewModel = new BugCreateViewModel
            {
                Projects = projects
            };
            return View(bugCreateViewModel);
        }

        [HttpPost]
        public IActionResult Create(BugCreateViewModel bugCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                var userName = User.FindFirst(ClaimTypes.Name)?.Value;

                var bug = new Bug
                {
                    ProjectId = bugCreateViewModel.ProjectId,
                    ProjectName = _context.Projects.FirstOrDefault(p => p.ProjectId == bugCreateViewModel.ProjectId)?.ProjectName,
                    BugName = bugCreateViewModel.BugName,
                    UserName = userName, 
                    CreateTime = DateTime.Now,
                    Status = BugStatus.New
                };

                _context.Bugs.Add(bug);
                _context.SaveChanges();

                var message = new Message
                {
                    BugId = bug.BugId,
                    UserName = userName,
                    Messages = bugCreateViewModel.Messages,
                    Time = DateTime.Now
                };

                _context.Messages.Add(message);
                _context.SaveChanges();

                return RedirectToAction("Index", "Bug");
            }

            var projects = _context.Projects.Select(project => new SelectListItem
            {
                Value = project.ProjectId.ToString(),
                Text = project.ProjectName
            }).ToList();

            bugCreateViewModel.Projects = projects;
            return View(bugCreateViewModel);
        }


        public IActionResult Details(int id)
        {
            var bugWithMessages = _context.Bugs
                .Include(b => b.Messages)
                .FirstOrDefault(b => b.BugId == id);

            if (bugWithMessages == null)
            {
                return NotFound();
            }

            return View(bugWithMessages);
        }


        [HttpPost]
        public IActionResult AddMessage(int bugId, string messages)
        {
            var bug = _context.Bugs.Include(b => b.Messages).FirstOrDefault(b => b.BugId == bugId);
            var userName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (bug != null)
            {
                bug.Messages.Add(new Message
                {
                    BugId = bug.BugId,
                    UserName = userName,
                    Messages = messages,
                    Time = DateTime.Now
                });
                _context.SaveChanges();
            }
            return RedirectToAction("Details", new { id = bugId });
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public IActionResult ChangeStatus(int bugId, string status)
        {
            var bug = _context.Bugs.FirstOrDefault(b => b.BugId == bugId);
            if (bug != null)
            {
                bug.Status = (BugStatus)Enum.Parse(typeof(BugStatus), status);
                _context.SaveChanges();
            }
            return RedirectToAction("Details", new { id = bugId });
        }
    }
}
