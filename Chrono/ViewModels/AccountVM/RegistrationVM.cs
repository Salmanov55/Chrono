using System.ComponentModel.DataAnnotations;

namespace Chrono.ViewModels.AccountVM
{
    public class RegistrationVM
    {
        [System.ComponentModel.DataAnnotations.Required, StringLength(100)]
        public string FullName { get; set; }
        [System.ComponentModel.DataAnnotations.Required, StringLength(100)]
        public string UserName { get; set; }
        [System.ComponentModel.DataAnnotations.Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [System.ComponentModel.DataAnnotations.Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [System.ComponentModel.DataAnnotations.Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string RepeatPassword { get; set; }

    }
}
