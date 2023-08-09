using PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Interviews;
using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels;
using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels.Interviews;
using PreUni.StaffManagement.Domain.Entities;

namespace PreUni.StaffManagement.Core.Interfaces.Services.Interviews
{
    public interface ITFNDeclareService
    {
        Task<ResponseModel<TFNDeclare>> GetTFNDeclareByJobApplicationId(int jobApplicationId);
        Task<ResponseModel> Create(TFNDeclare request);
        Task<ResponseModel<TFNDeclare>> GetByJobId(int Id);
        Task<ResponseModel> Update(TFNDeclare model, int jobApplicationId);
    }
}
