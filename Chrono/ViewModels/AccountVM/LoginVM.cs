using System.ComponentModel.DataAnnotations;

namespace Chrono.ViewModels.AccountVM
{
    public class LoginVM
    {
        [System.ComponentModel.DataAnnotations.Required, StringLength(100)]
        public string UserNameOrEmail { get; set; }

        [System.ComponentModel.DataAnnotations.Required, DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememmberMe { get; set; }
    }
}
