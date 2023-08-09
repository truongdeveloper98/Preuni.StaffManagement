
using System.ComponentModel.DataAnnotations;

namespace PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Authentications
{
    public class LoginRequestViewModel
    {
        [Required]
        public string Email {get;set;} = string.Empty;
        [Required]
        public string Password {get;set;} =  string.Empty;
    }
}
