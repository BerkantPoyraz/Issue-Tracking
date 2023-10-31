using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Issues.Models.Bug;

namespace Issues.Models
{
    public class BugCreateViewModel
    {
        public int ProjectId { get; set; }
        public IEnumerable<SelectListItem>? Projects { get; set; }
        public int BugId1 { get; set; }
        public string BugName { get; set; } = string.Empty;
        public string Messages { get; set; } = string.Empty;
        public BugStatus Status { get; set; }
    }
}
