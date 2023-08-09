using Microsoft.AspNetCore.Identity;
using PreUni.StaffManagement.Core.Interfaces.Repositories.Uses;
using PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Authentications;
using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels;
using PreUni.StaffManagement.Domain.Entities;
using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels.User;
using PreUni.StaffManagement.Core.Constants.Users;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using PreUni.StaffManagement.Core.Interfaces.Authentications;
using PreUni.StaffManagement.Core.Interfaces.Services.Users;

namespace PreUni.StaffManagement.Core.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAuthenticationService _authenticationService;

        public UserService(IUserRepository userRepository, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IAuthenticationService authenticationService)
        {
           _userRepository = userRepository;
           _userManager = userManager;
           _roleManager = roleManager;
           _authenticationService = authenticationService;
        }

        public async Task<PagedResponseDto<UserResponse>> GetUserList(string sortBy, string search, int currentPage, int pageSize, string sortOrder)
        {
            var userQuery = _userRepository
                .Query()
                .Where(i => !i.IsDeleted && i.IsActive)
                .AsQueryable();

            
            if (!string.IsNullOrEmpty(search))
            {
                userQuery = userQuery.Where(i => i.FullName.Contains(search));
            }

            var totalUsers = userQuery.Where(i => !i.IsDeleted).Count();

            if (sortOrder == "DESC")
            {
                switch(sortBy)
                {
                    case "FirstName":
                        userQuery = userQuery.OrderByDescending(i => i.FullName);
                        break;
                    default:
                        userQuery = userQuery.OrderByDescending(i => i.CreatedAt);
                        break;
                }
            }
            else
            {
                switch(sortBy)
                {
                    case "FirstName":
                        userQuery = userQuery.OrderBy(i => i.FullName);
                        break;
                    default:
                        userQuery = userQuery.OrderBy(i => i.CreatedAt);
                        break;
                }
            }

            userQuery = userQuery
                .Skip(pageSize * currentPage)
                .Take(pageSize);

            var users = userQuery
                .Select(i => new UserResponse
                {
                    Id = i.Id,
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    IsActive = i.IsActive,
                    IdentityId = i.IdentityId
                })
                .ToList();

            var userIds = users.Select(i => i.Id).ToList();

            var identityList = await _userRepository.GetIdentityUserListByUserIds(userIds);

            foreach(var user in users)
            {
                var targetIdentityUser = identityList.Data != default ? identityList.Data.FirstOrDefault(i => i.Id == user.IdentityId) : default;
                user.Email = targetIdentityUser != default && targetIdentityUser.Email != default ? targetIdentityUser.Email : "";
                user.PhoneNumber = targetIdentityUser != default && targetIdentityUser.PhoneNumber != default ? targetIdentityUser.PhoneNumber : "";
            }

            return new PagedResponseDto<UserResponse>
            {
                Page = currentPage,
                Limit = pageSize,
                TotalItems = totalUsers,
                TotalPages = (int)(Math.Ceiling((double)(totalUsers / pageSize))),
                Items = users
            };
        }

        public async Task<ResponseModel<UserResponse>> GetUserById(int id)
        {
            var user = await _userRepository.Get(id);

            if (user == default)
            {
                throw new Exception("Cannot find user.");
            }

            var identity = await _userRepository.GetIdentityUserByUserId(id);

            if (identity == default || identity.Data == default)
            {
                throw new Exception("Cannot find identity user.");
            }

            var appUserRole = await _userManager.GetRolesAsync(identity.Data);

            if (appUserRole == default)
            {
                throw new Exception("Something wrong!");
            }

            var responseUser = new UserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = identity.Data.Email != default ? identity.Data.Email : "",
                PhoneNumber = identity.Data.PhoneNumber != default ? identity.Data.PhoneNumber : "",
                IsActive = user.IsActive,
                Roles = appUserRole.ToList()
            };

            return new ResponseModel<UserResponse>()
            {
                Status = 1,
                Message = "Get user by id successful!",
                Data = responseUser
            };
        }

        public async Task<ResponseModel<User>> CreateNewUser(RegisterRequestViewModel requestViewModel)
        {
            // Create new instance for User Entity
            User user = new User()
            {
                FirstName = requestViewModel.FirstName,
                LastName = requestViewModel.LastName,
                FullName = requestViewModel.FirstName + " " + requestViewModel.LastName,    
                IdentityId = requestViewModel.IdentityUser
			};

            // call method create new bankccount from JobApplication
            await _userRepository.Add(user);

            // return ResponseModel with status = 1 when create success
            return new ResponseModel<User>()
            {
                Status = 1,
                Message = UserConstant.CreateSuccessed,
                Data = user
            };
        }

        public async Task<ResponseModel<UserResponse>> CreateNewUserManually(UpdateUserRequestViewModel requestViewModel)
        {
            if (requestViewModel.Roles.Length == 0)
            {
                throw new Exception("Roles is required");
            }

            if (_userManager.FindByEmailAsync(requestViewModel.Email).Result != null)
            {
                throw new Exception("Duplicate Email");
            }

            var email = new EmailAddressAttribute();
            if (!email.IsValid(requestViewModel.Email))
            {
                throw new Exception("Invalid email");
            }

            string defaultPassword = "Asdfgh1@3";

            var createUserRequest = new RegisterRequestViewModel() 
            {
               FirstName = requestViewModel.FirstName,
               LastName = requestViewModel.LastName,
               Email = requestViewModel.Email,
               Password =  defaultPassword,
               PhoneNumber = requestViewModel.PhoneNumber
            };

            var responseUser = new UserResponse();

            var result = await _authenticationService.RegisterAsync(createUserRequest);

            if (result.Id != default)
            {
                var user = await _userManager.FindByIdAsync(result.Id.ToString());

                if (user == default)
                {
                    throw new Exception("Cannot find user with roles.");
                }

                var roles = await _userManager.GetRolesAsync(user);

                if (roles == default)
                {
                    throw new Exception("Something wrong!");
                }

                var resultUser = new User
                {
                    FirstName = requestViewModel.FirstName,
                    LastName = requestViewModel.LastName,
                    FullName = requestViewModel.FirstName + " " + requestViewModel.LastName,
                    IsActive = true,
                    IdentityId = result.Id
                };

                await _userRepository.Add(resultUser);

                responseUser.FirstName = requestViewModel.FirstName;
                responseUser.LastName = requestViewModel.LastName;
                responseUser.Email = requestViewModel.Email;
                responseUser.PhoneNumber = requestViewModel.PhoneNumber != default ? requestViewModel.PhoneNumber : "";
                responseUser.IsActive = true;
                responseUser.Roles = roles.ToList();
            }

            return new ResponseModel<UserResponse>()
            {
                Status = 1,
                Message = UserConstant.CreateSuccessed,
                Data = responseUser
            };
        }

        public async Task<ResponseModel<UserResponse>> UpdateUser(UpdateUserRequestViewModel model, int id)
        {
            var user = await _userRepository.Get(id);

            if (user is null)
            {
                throw new Exception($"{UserConstant.UpdateFailed}. Cannot find user.");
            }

            var identity = await _userManager.FindByIdAsync(user.IdentityId.ToString());

            if (identity == default)
            {
                throw new Exception("Cannot find identity user.");
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.FullName = $"{model.FirstName} {model.LastName}";
            identity.PhoneNumber = model.PhoneNumber != default ? model.PhoneNumber : "";
            identity.Email = model.Email;

            await _userRepository.Update(user);

            return new ResponseModel<UserResponse>
            {
                Status = 1,
                Message = UserConstant.UpdateSuccessed,
                Data = new UserResponse
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    IsActive = user.IsActive,
                    Email = identity.Email,
                    PhoneNumber = identity.PhoneNumber != default ? identity.PhoneNumber : ""
                }
            };
        }

        public async Task<ResponseModel<bool>> DeleteUser(int id)
        {
            var user = await _userRepository.Get(id);

            if (user == default)
            {
                throw new Exception($"{UserConstant.DeleteFailed}. Cannot find user.");
            }

            var identity = await _userRepository.GetIdentityUserByUserId(id);

            if (identity == default || identity.Data == default)
            {
                throw new Exception("Cannot find identity user.");
            }

            await _userRepository.Delete(user);
            await _userManager.DeleteAsync(identity.Data);

            return new ResponseModel<bool>
            {
                Status = 1,
                Message = UserConstant.DeleteSuccessed,
                Data = true
            };
        }

		/// <summary>
		/// Method update image for user
		/// </summary>
		/// <param name="JobApplicationId"></param>
		/// <param name="pathImage"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public async Task<ResponseModel> UpdateImage(int userId, IFormFile imageFile)
		{
			// find user by user Id
			var result = await _userRepository.Get(userId);

			// if user not exist throw not found user
			if (result is null)
			{
				throw new Exception("User not found");
			}

			//upload file
			string relativePath = $"images/{imageFile.FileName}";
			string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath);
			var filePath = Path.GetTempFileName();

			using (var stream = System.IO.File.Create(path))
			{
				await imageFile.CopyToAsync(stream);
			}

			result.Image = relativePath;
			result.UpdatedAt = DateTime.Now;
			await _userRepository.Update(result);
			return new ResponseModel()
			{
				Status = 1,
				Message = "Update Image For User Success"
			};
		}

		public Task<ResponseModel<UserImageResponse>> GetImageUserById(int id)
		{
            var result = _userRepository.GetImageUserById(id);
            return result;
		}

        public async Task<int> GetUserByIdentityId(int id)
        {
            // get userid by identityid
           return await _userRepository.GetUserByIdentityId(id);
        }

        public async Task<ResponseModel<UserResponse>> GetUserByUserName(string userName)
        {
            var identityUser = await _userManager.FindByEmailAsync(userName);

            if (identityUser == default)
            {
                throw new Exception("Cannot find identity user by user name");
            }

            var user = _userRepository.Find(i => i.IdentityId == identityUser.Id).FirstOrDefault();

            if (user == default)
            {
                throw new Exception("Something wrong!");
            }

            var roles = await _userManager.GetRolesAsync(identityUser);

            var responseUser = new UserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = identityUser.Email != default ? identityUser.Email : "",
                PhoneNumber = identityUser.PhoneNumber != default ? identityUser.PhoneNumber : "",
                IsActive = user.IsActive,
                Roles = roles.ToList()
            };

            return new ResponseModel<UserResponse>()
            {
                Status = 1,
                Message = "Get user by username successful!",
                Data = responseUser
            };
        }       
    }
}
