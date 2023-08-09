using PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Interviews;
using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels;
using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels.Interviews;
using PreUni.StaffManagement.Domain.Entities;

namespace PreUni.StaffManagement.Core.Interfaces.Services.Interviews
{
    public interface IBankAccountService
    {
        Task<ResponseModel<BankAccountDetail>> GetBankAccountById(int jobApplicationId);
        Task<ResponseModel> Create(BankAccountDetail request);

        Task<ResponseModel<BankAccountDetail>> GetByJobId(int Id);
        Task<ResponseModel> Update(BankAccountDetail model, int jobApplicationId);
    }
}
