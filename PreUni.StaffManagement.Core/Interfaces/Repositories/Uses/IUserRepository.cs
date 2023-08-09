using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels;
using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels.User;
using PreUni.StaffManagement.Domain.Entities;

namespace PreUni.StaffManagement.Core.Interfaces.Repositories.Uses
{
    public interface IUserRepository : IRepository<User>
    {
        //AppUser? GetUserByUserName(string userName);
        Task<ResponseModel<UserImageResponse>> GetImageUserById(int Id);
        Task<ResponseModel<AppUser>> GetIdentityUserByUserId(int userId);
        Task<ResponseModel<List<AppUser>>> GetIdentityUserListByUserIds(List<int> userId);

        Task<int> GetUserByIdentityId(int Id);
    }
}
