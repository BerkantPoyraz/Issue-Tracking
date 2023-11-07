using Issues.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Issue.ViewModel
{
    public class BugCreateViewModel
    {
        public int ProjectId { get; set; }
        public IEnumerable<SelectListItem> Projects { get; set; }
        public string BugName { get; set; } = string.Empty;
        public string Messages { get; set; } = string.Empty;
        public BugStatus Status { get; set; }
    }
}
