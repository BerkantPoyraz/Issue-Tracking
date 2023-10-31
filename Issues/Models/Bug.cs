using Microsoft.AspNetCore.Identity;

namespace Issues.Models
{
    public class Bug
    {
        public enum BugStatus
        {
            New,
            InProgress,
            Closed
        }
        public int BugId { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public string BugName { get; set; } = string.Empty;
        public virtual List<Message> Messages { get; set; } = new List<Message>();
        public string UserName { get; set; } 
        public IdentityUser User { get; set; }
        public  DateTime CreateTime { get; set; }
        public BugStatus Status { get; set; }
    }
}
