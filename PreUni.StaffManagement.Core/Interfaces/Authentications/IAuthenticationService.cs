using PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Authentications;
using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels.Authentications;

namespace PreUni.StaffManagement.Core.Interfaces.Authentications
{
    public interface IAuthenticationService
    {

        Task<AuthenticationResponse> LoginAsync(LoginRequestViewModel model, bool adminWeb=false);
        Task<AuthenticationResponse> RegisterAsync(RegisterRequestViewModel model);
    }
}
