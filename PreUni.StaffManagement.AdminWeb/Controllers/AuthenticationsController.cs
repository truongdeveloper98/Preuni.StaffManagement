using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PreUni.StaffManagement.Core.Constants.Systems;
using PreUni.StaffManagement.Core.Interfaces.Authentications;
using PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Authentications;

namespace PreUni.StaffManagement.AdminWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationsController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationsController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Login Method
        /// </summary>
        /// <param name="loginRequestViewModel"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginRequestViewModel loginRequestViewModel)
        {
            // if model not valid return bad request
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // get token when user login in systerm
            var token = await _authenticationService.LoginAsync(loginRequestViewModel,true);

            // return result
            return Ok(token);
        }   
    }
}
