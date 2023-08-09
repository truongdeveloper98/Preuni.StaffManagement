using Microsoft.EntityFrameworkCore;
using PreUni.StaffManagement.Core.Interfaces.Repositories.Uses;
using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels;
using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels.User;
using PreUni.StaffManagement.Domain.Entities;
using PreUni.StaffManagement.Infrastructure.EntityFramework;

namespace PreUni.StaffManagement.Infrastructure.Repositories.Users
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {

        }

		public async Task<ResponseModel<AppUser>> GetIdentityUserByUserId(int userId)
		{
			var identityId = await _context.Users
				.Where(i => !i.IsDeleted && i.IsActive && i.Id == userId)
				.Select(i => i.IdentityId)
				.FirstOrDefaultAsync();

			var result = await _context.AppUsers
				.Where(i => i.Id == identityId)
				.FirstOrDefaultAsync();

			return new ResponseModel<AppUser>()
			{
				Status = 1,
				Message = "Success!",
				Data = result
			};
		}

		public async Task<ResponseModel<List<AppUser>>> GetIdentityUserListByUserIds(List<int> userId)
		{
			var identityIds = await _context.Users
				.Where(i => !i.IsDeleted && i.IsActive && userId.Contains(i.Id))
				.Select(i => i.IdentityId)
				.ToListAsync();

			var result = await _context.AppUsers
				.Where(i => identityIds.Contains(i.Id))
				.ToListAsync();

			return new ResponseModel<List<AppUser>>()
			{
				Status = 1,
				Message = "Success!",
				Data = result
			};
		}

		public async Task<ResponseModel<UserImageResponse>> GetImageUserById(int Id)
		{
			var result = await _context.Users.Select(x=> new UserImageResponse {
				Id = x.Id,
				Image = x.Image,
			}).FirstOrDefaultAsync(x => x.Id == Id);

			if(result is null)
			{
				return new ResponseModel<UserImageResponse>()
				{
					Status = 0,
					Message = "User Not Found",
					Data = null
				};
			}

			return new ResponseModel<UserImageResponse>()
			{
				Status = 1,
				Message = "Get User Success",
				Data = result
			};
		}

        public async Task<int> GetUserByIdentityId(int Id)
        {
            return await _context.Users
				                 .Where(x => x.IdentityId == Id)
								 .Select(x => x.Id)
								 .FirstOrDefaultAsync();
        }
    }
}
