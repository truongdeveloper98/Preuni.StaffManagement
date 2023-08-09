using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PreUni.StaffManagement.Domain.Entities
{
    public class AppUser : IdentityUser<int>
    {
    }
}