using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Authentications
{
    public class NewUserRequestViewModel
    {

        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string JobTitle { get; set; } = default!;

        public string Email { get; set; } = default!;
        public string? PhoneNumber { get; set; } = default!;
    }
}
