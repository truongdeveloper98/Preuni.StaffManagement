using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels;
using PreUni.StaffManagement.Domain.Entities;

namespace PreUni.StaffManagement.Core.Interfaces;

public interface ISuperAnnuationService
{
    Task<ResponseModel> Create(SuperAnnuation request);
    Task<SuperAnnuation> GetOne(int jobId);
    Task<ResponseModel> Update(int id,SuperAnnuation request);
}