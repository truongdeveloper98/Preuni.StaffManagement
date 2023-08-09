using PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Interviews;
using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels;
using PreUni.StaffManagement.Domain.Entities;

namespace PreUni.StaffManagement.Core.Interfaces.Repositories.Interviews
{
    public interface ITFNDeclareRepository : IRepository<TFNDeclare>
    {
        Task<ResponseModel<TFNDeclare>> GetByJobApplicationId(int id);
    }
}
