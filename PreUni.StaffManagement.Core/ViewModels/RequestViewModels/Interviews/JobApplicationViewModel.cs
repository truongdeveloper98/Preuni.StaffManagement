using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Interviews
{
    public class JobApplicationViewModel
    {
        public int UserId { get; set; } = default!;
        public string JobTitle { get; set; } = default!;
    }
}
