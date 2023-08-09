using Microsoft.EntityFrameworkCore;
using PreUni.StaffManagement.Core.Interfaces.Repositories.Interviews;
using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels;
using PreUni.StaffManagement.Domain.Entities;
using PreUni.StaffManagement.Infrastructure.EntityFramework;

namespace PreUni.StaffManagement.Infrastructure.Repositories.Interviews
{
    public class JobApplicationRespository : RepositoryBase<JobApplication>, IJobApplicationRespository
    {
        public JobApplicationRespository(ApplicationDbContext context) : base(context) { }

		/// <summary>
		/// Method using get jobappication by userid
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public async Task<ResponseModel<JobApplication>> GetByUserId(int userId)
		{
			// find job application by user id
			var jobApplicant = await _context.JobApplications.FirstOrDefaultAsync(x => x.UserId == userId);

			// if jobapplication with id not exists
			if (jobApplicant is null)
			{
				// return model with data is null
				return new ResponseModel<JobApplication>()
				{
					Status = 1,
					Message = string.Empty,
					Data = null
				};
			}

			// if jobapplication with id exists return model with data = result
			return new ResponseModel<JobApplication>()
			{
				Status = 1,
				Message = string.Empty,
				Data = jobApplicant
			};
		}
	}
}
