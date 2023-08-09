using PreUni.StaffManagement.Core.Constants.Interviews;
using PreUni.StaffManagement.Core.Interfaces.Repositories.Interviews;
using PreUni.StaffManagement.Core.Interfaces.Services.Interviews;
using PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Interviews;
using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels;
using PreUni.StaffManagement.Domain.Entities;

namespace PreUni.StaffManagement.Infrastructure.Services.Interviews
{
    public class JobApplicationService : IJobAppicationService
    {
        private readonly IJobApplicationRespository _jobApplicationRespository;

        public JobApplicationService(IJobApplicationRespository jobApplicationRespository)
        {
            _jobApplicationRespository = jobApplicationRespository;
        }

        /// <summary>
        /// Create new Bank Account Entity
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseModel<JobApplication>> Create(JobApplicationViewModel request)
        {
            // Create new instance for JobApplication Entity
            JobApplication jobApplication = new JobApplication()
            {
                UserId = request.UserId,
                JobTitle = request.JobTitle,
                AppliedDate = DateTime.Now
            };

            // call method create new bankccount from JobApplication
            await _jobApplicationRespository.Add(jobApplication);

            // return ResponseModel with status = 1 when create success
            return new ResponseModel<JobApplication>()
            {
                Status = 1,
                Message = JobApplicationConstant.CreateSuccessed,
                Data = jobApplication
            };
        }

		public async Task<JobApplication> GetByJobId(int id)
		{
            var result = await _jobApplicationRespository.Get(id);
            if(result is null)
            {
                throw new Exception("JobApplication not found");
            }
            return new JobApplication 
            { 
                UserId = result.UserId,
            };

        }

        /// <summary>
        /// Method using get jobapplication by user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResponseModel<JobApplication>> GetByUserId(int userId)
		{
            // find job application by id
			var result = await _jobApplicationRespository.GetByUserId(userId);

            return result;
		}
	}
}
