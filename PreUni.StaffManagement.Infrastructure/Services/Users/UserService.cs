// using PreUni.StaffManagement.Core.Constants.Interviews;
// using PreUni.StaffManagement.Core.Interfaces.Repositories.Uses;
// using PreUni.StaffManagement.Core.Interfaces.Users;
// using PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Authentications;
// using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels;
// using PreUni.StaffManagement.Domain.Entities;

// namespace PreUni.StaffManagement.Infrastructure.Services.Users
//{
//     public class UserService : IUserService
//     {
//         private readonly IUserRepository _userRepository;

//         public UserService(IUserRepository userRepository)
//         {
//            _userRepository = userRepository;
//         }
//         /// <summary>
//         /// Method create new user
//         /// </summary>
//         /// <param name="requestViewModel"></param>
//         /// <returns></returns>
//         public async Task<ResponseModel<User>> CreateNewUser(NewUserRequestViewModel requestViewModel)
//         {
//             // Create new instance for User Entity
//             User user = new User()
//             {
//                 FirstName = requestViewModel.FirstName,
//                 LastName = requestViewModel.LastName,
//                 FullName = requestViewModel.FirstName + " " + requestViewModel.LastName,    
// 			};

//             // call method create new bankccount from JobApplication
//             await _userRepository.Add(user);

//             // return ResponseModel with status = 1 when create success
//             return new ResponseModel<User>()
//             {
//                 Status = 1,
//                 Message = JobAppicationConstant.CreateSuccessed,
//                 Data = user
//             };
//         }

        
// 	}
// }
