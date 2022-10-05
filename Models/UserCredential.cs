using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class UserCredential
    {
        [Requiered]
        public string UserName { get; set; }
        [Requiered]
        [EmailAddress]
        public string Email { get; set; }
        [Requiered]
        public string Password { get; set; }
    }
}
