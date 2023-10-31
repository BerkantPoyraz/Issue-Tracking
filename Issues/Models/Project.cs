using System.ComponentModel.DataAnnotations;

namespace Issues.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Project Name is Required")]
        public string ProjectName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Project Description is Required")]
        public string ProjectDescription { get; set; } = string.Empty;
    }
}
