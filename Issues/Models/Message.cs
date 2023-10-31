using Microsoft.AspNetCore.Identity;

namespace Issues.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public int BugId { get; set; }
        public Bug Bug { get; set; }
        public string Messages { get; set; }
        public string UserName { get; set; }
        public DateTime Time { get; set; }
    }
}
