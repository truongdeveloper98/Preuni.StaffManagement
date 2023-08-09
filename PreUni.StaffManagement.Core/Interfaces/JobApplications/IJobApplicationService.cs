using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels;
using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels.JobApplications;
using PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Interviews;

namespace PreUni.StaffManagement.Core.Interfaces.JobApplications
{
    public interface IJobApplicationService
    {
        Task<PagedResponseDto<JobApplicationResponse>> GetJobApplications(string sortBy, string search, int currentPage, int pageSize, string sortOrder);
        Task<ResponseModel<JobApplicationResponse>> GetJobApplicationById(int id);
        Task<ResponseModel<JobApplicationResponse>> CreateJobApplicationManual(JobApplicationRequestModel model);
        Task<ResponseModel<bool>> UpdateJobApplication(JobApplicationRequestModel model, int id);
        Task<ResponseModel<bool>> DeleteJobApplication(int id);
    }
}
