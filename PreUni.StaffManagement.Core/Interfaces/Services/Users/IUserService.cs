using PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Authentications;
using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels;
using PreUni.StaffManagement.Domain.Entities;
using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels.User;
using Microsoft.AspNetCore.Http;

namespace PreUni.StaffManagement.Core.Interfaces.Services.Users
{
    public interface IUserService
    {
        Task<ResponseModel<User>> CreateNewUser(RegisterRequestViewModel requestViewModel);
        Task<ResponseModel<UserResponse>> GetUserById(int id);
        Task<PagedResponseDto<UserResponse>> GetUserList(string sortBy, string search, int currentPage, int pageSize, string sortOrder);
        Task<ResponseModel<UserResponse>> CreateNewUserManually(UpdateUserRequestViewModel requestViewModel);
        Task<ResponseModel<UserResponse>> UpdateUser(UpdateUserRequestViewModel model, int id);
        Task<ResponseModel<bool>> DeleteUser(int id);

        Task<ResponseModel> UpdateImage(int userId, IFormFile formFile);

        Task<ResponseModel<UserImageResponse>> GetImageUserById(int id);

        Task<int> GetUserByIdentityId(int id);
        Task<ResponseModel<UserResponse>> GetUserByUserName(string userName);
    }
}
