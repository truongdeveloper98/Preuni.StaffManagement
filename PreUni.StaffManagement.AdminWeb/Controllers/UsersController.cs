using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PreUni.StaffManagement.Core.Interfaces.Services.Users;
using PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Authentications;

namespace PreUni.StaffManagement.AdminWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("/api/users")]
        public async Task<IActionResult> GetUserList([FromQuery] UserListRequestViewModel model)
        {
            try
            {
                var result = await _userService.GetUserList(model.sortColumnName, model.textSearch, model.currentPage, model.pageSize, model.sortColumnDirection);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("/api/users/{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            try
            {
                var result = await _userService.GetUserById(id);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("/api/users")]
        public async Task<IActionResult> CreateUserManually([FromBody] UpdateUserRequestViewModel model)
        {
            try
            {
                var result = await _userService.CreateNewUserManually(model);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        [Route("/api/users/{id}")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequestViewModel model, [FromRoute] int id)
        {
            try
            {
                var result = await _userService.UpdateUser(model, id);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete]
        [Route("/api/users/{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            try
            {
                var result = await _userService.DeleteUser(id);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}