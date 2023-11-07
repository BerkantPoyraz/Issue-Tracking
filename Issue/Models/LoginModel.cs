using System.ComponentModel.DataAnnotations;

namespace Issues.Models
{
    public class LoginModel
    {
        private string _returnurl;

        [Required(ErrorMessage = "Name is Required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Password is Required.")]
        public string Password { get; set; }

        public string ReturnUrl 
        { 
            get
            {
                if (_returnurl == null)
                    return "/";
                else
                    return _returnurl;
            }
            set
            {
                _returnurl = value;
            }
        }
    }
}
