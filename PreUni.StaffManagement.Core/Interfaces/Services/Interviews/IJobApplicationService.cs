using PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Interviews;
using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels;
using PreUni.StaffManagement.Domain.Entities;

namespace PreUni.StaffManagement.Core.Interfaces.Services.Interviews
{
    public interface IJobAppicationService
    {
		Task<ResponseModel<JobApplication>> Create(JobApplicationViewModel request);

		Task<ResponseModel<JobApplication>> GetByUserId(int userId);

		Task<JobApplication> GetByJobId(int id);

	}
}
