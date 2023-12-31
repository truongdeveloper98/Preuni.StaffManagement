﻿
namespace PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Authentications
{
    public class RegisterRequestViewModel
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string? PhoneNumber { get; set; } = default!;
        public int IdentityUser { get; set; } = default!;
    }
}
