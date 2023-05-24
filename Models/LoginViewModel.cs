using System.ComponentModel.DataAnnotations;

namespace LogsMonitoring.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Pole Nazwa użytkownika jest wymagane.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Pole Hasło jest wymagane.")]
        public string Password { get; set; }
    }
}
