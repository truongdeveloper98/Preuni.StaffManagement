using Microsoft.AspNetCore.Identity;

namespace PreUni.StaffManagement.Core.CustomExcceptions
{
    public class RegistrationException: Exception
    {
        public RegistrationException(IEnumerable<IdentityError> errors)
        {
            Errors = errors;
        }

        public IEnumerable<IdentityError>? Errors { get; set;} = default;
    }
}
