using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels;
using PreUni.StaffManagement.Domain.Entities;

namespace PreUni.StaffManagement.Core.Interfaces.Repositories.Interviews
{
    public interface IJobApplicationRespository : IRepository<JobApplication>
    {
		Task<ResponseModel<JobApplication>> GetByUserId(int userId);
	}
}
