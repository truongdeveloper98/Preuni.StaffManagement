using PreUni.StaffManagement.Core.Interfaces.JobApplications;
using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels;
using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels.JobApplications;
using PreUni.StaffManagement.Core.Interfaces.Repositories.Interviews;
using PreUni.StaffManagement.Core.Constants.Interviews;
using PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Interviews;
using PreUni.StaffManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using PreUni.StaffManagement.Core.Interfaces.Repositories.Uses;
using PreUni.StaffManagement.Domain.Enum;

namespace PreUni.StaffManagement.Core.Services.JobApplications
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IJobApplicationRespository _jobApplicationRepo;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;

        public JobApplicationService(IJobApplicationRespository jobApplicationRespository, IUserRepository userRepository, UserManager<AppUser> userManager)
        {
            _jobApplicationRepo = jobApplicationRespository;
            _userRepository = userRepository;
            _userManager = userManager;
        }
        public async Task<PagedResponseDto<JobApplicationResponse>> GetJobApplications(string sortBy, string search, int currentPage, int pageSize, string sortOrder)
        {
            var query = _jobApplicationRepo.Query(includeProperties: "User");

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(i => (i.User.FirstName + " " + i.User.LastName).Contains(search));
            }

            var totalJobApplications = query.Count();

            if (sortOrder == "DESC")
            {
                query = sortBy switch
                {
                    "FirstName" => query.OrderByDescending(i => i.User.FirstName),
                    "LastName" => query.OrderByDescending(i => i.User.LastName),
                    _ => query.OrderByDescending(i => i.CreatedAt),
                };
            }
            else
            {
                query = sortBy switch
                {
                    "FirstName" => query.OrderBy(i => i.User.FirstName),
                    "LastName" => query.OrderBy(i => i.User.LastName),
                    _ => query.OrderBy(i => i.CreatedAt),
                };
            }

            var jobApplications = query
                .Skip(currentPage * pageSize)
                .Take(pageSize)
                .Select(i => new JobApplicationResponse
                {
                    Id = i.Id,
                    FirstName = i.User.FirstName,
                    LastName = i.User.LastName,
                    JobTitle = i.JobTitle,
                    Status = i.Status,
                    AppliedDate = i.AppliedDate
                })
                .ToList();

            return new PagedResponseDto<JobApplicationResponse>()
            {
                Page = currentPage,
                Limit = pageSize,
                TotalItems = totalJobApplications,
                TotalPages = (int)(Math.Ceiling((double)(totalJobApplications / pageSize))),
                Items = jobApplications
            };
        }

        public async Task<ResponseModel<JobApplicationResponse>> GetJobApplicationById(int id)
        {
            var jobApplication = await _jobApplicationRepo.Get(id, includeProperties: "User");

            if (jobApplication == default)
            {
                throw new Exception("Cannot find job application");
            }

            var identity = await _userManager.FindByIdAsync(jobApplication.User.IdentityId.ToString());

            if (identity == default)
            {
                throw new Exception("Cannot find user identity");
            }

            var result = new JobApplicationResponse
            {
                FirstName = jobApplication.User.FirstName,
                LastName = jobApplication.User.LastName,
                JobTitle = jobApplication.JobTitle,
                Email = identity.Email != default ? identity.Email : "",
                PhoneNumber = identity.PhoneNumber != default ? identity.PhoneNumber : "",
                AppliedDate = jobApplication.AppliedDate,
                Status = jobApplication.Status
            };

            return new ResponseModel<JobApplicationResponse>
            {
                Status = 1,
                Message = "Get job application by id successful!",
                Data = result
            };
        }

        public async Task<ResponseModel<JobApplicationResponse>> CreateJobApplicationManual(JobApplicationRequestModel model)
        {
            var identity = await _userManager.FindByEmailAsync(model.Email);

            if (identity == default)
            {
                throw new Exception("Cannot find the user!");
            }

            var identityId = identity.Id;

            var user = _userRepository.Query().FirstOrDefault(i => i.IdentityId == identityId);

            if (user == default)
            {
                throw new Exception("Something wrong with the user!");
            }

            JobApplication jobApplication = new JobApplication()
            {
                UserId = user.Id,
                Status = model.Status,
                AppliedDate = model.AppliedDate,
                JobTitle = model.JobTitle
            };

            await _jobApplicationRepo.Add(jobApplication);

            var result = new JobApplicationResponse
            {
                Id = jobApplication.Id,
                JobTitle = jobApplication.JobTitle,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = identity.Email == default ? "" : identity.Email,
                Status = model.Status,
                AppliedDate = model.AppliedDate,
            };

            return new ResponseModel<JobApplicationResponse>()
            {
                Status = 1,
                Message = "Create job application successful!",
                Data = result
            };
        }

        public async Task<ResponseModel<bool>> UpdateJobApplication(JobApplicationRequestModel model, int id)
        {
            var jobApplication = await _jobApplicationRepo.Get(id, includeProperties: "User");

            if (jobApplication == default)
            {
                throw new Exception("Cannot find job application");
            }

            jobApplication.JobTitle = model.JobTitle;
            jobApplication.User.FirstName = model.FirstName;
            jobApplication.User.LastName = model.LastName;
            jobApplication.AppliedDate = model.AppliedDate;
            jobApplication.Status = model.Status;

            return new ResponseModel<bool>
            {
                Status = 1,
                Message = "Create job application successful!",
                Data = true
            };
        }

        public async Task<ResponseModel<bool>> DeleteJobApplication(int id)
        {
            var jobApplication = await _jobApplicationRepo.Get(id);

            if (jobApplication == default)
            {
                throw new Exception($"{JobApplicationConstant.DeleteFailed}. Cannot find job application.");
            }

            await _jobApplicationRepo.Delete(jobApplication);

            return new ResponseModel<bool>
            {
                Status = 1,
                Message = JobApplicationConstant.DeleteSuccessed,
                Data = true
            };
        }
    }
}