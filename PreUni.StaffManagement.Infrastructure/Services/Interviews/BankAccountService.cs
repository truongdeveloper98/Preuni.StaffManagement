using PreUni.StaffManagement.Core.Constants.Interviews;
using PreUni.StaffManagement.Core.Interfaces.Repositories.Interviews;
using PreUni.StaffManagement.Core.Interfaces.Services.Interviews;
using PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Interviews;
using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels;
using PreUni.StaffManagement.Domain.Entities;
using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels.Interviews;
using Microsoft.EntityFrameworkCore;

namespace PreUni.StaffManagement.Infrastructure.Services.Interviews
{
    public class BankAccountService : IBankAccountService
    {
        private readonly IBankAccountRepository _bankAccountRepository;

        public BankAccountService(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        /// <summary>
        /// Create new Bank Account Entity
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseModel<BankAccountDetail>> GetBankAccountById(int jobApplicationId)
        {
            var bankAccount = await _bankAccountRepository
                .Query()
                .Select(i => new BankAccountDetail
                {
                    BankName = i.BankName,
                    AccountHolderName = i.AccountHolderName,
                    BSBNumber = i.BSBNumber,
                    AccountNumber = i.AccountNumber,
                    EmailAddress = i.EmailAddress,
                    PhoneNumber = i.PhoneNumber,
                    SignDate = i.SignDate,
                    NameApplicant = i.NameApplicant,
                    Signature = i.Signature
                })
                .FirstOrDefaultAsync(i => i.JobApplicationId == jobApplicationId);

            return new ResponseModel<BankAccountDetail>
            {
                Status = 1,
                Message = "",
                Data = bankAccount
            };
        }


        public async Task<ResponseModel> Create(BankAccountDetail request)
        {
            // call method create new bankccount from BankAccountRepository
            await _bankAccountRepository.Add(request);

            // return ResponseModel with status = 1 when create success
            return new ResponseModel()
            {
                Status = 1,
                Message = BankAccountConstant.CreateSuccessed
            };
        }

		public Task<ResponseModel<BankAccountDetail>> GetByJobId(int Id)
		{
            var result = _bankAccountRepository.GetByJobApplicationId(Id);
            return result;
		}
        public async Task<ResponseModel> Update(BankAccountDetail model, int jobApplicationId)
        {
            var jobApplication = _bankAccountRepository
                .Query()
                .FirstOrDefault(i => i.JobApplicationId == jobApplicationId);

            if (jobApplication == default)
            {
                throw new Exception("Cannot find any job application");
            }

            jobApplication.BankName = model.BankName;
            jobApplication.AccountHolderName = model.AccountHolderName;
            jobApplication.BSBNumber = model.BSBNumber;
            jobApplication.AccountNumber = model.AccountNumber;
            jobApplication.EmailAddress = model.EmailAddress;
            jobApplication.PhoneNumber = model.PhoneNumber;
            jobApplication.NameApplicant = model.NameApplicant;
            jobApplication.Signature = model.Signature;
            jobApplication.SignDate = model.SignDate;
            

            await _bankAccountRepository.Update(jobApplication);

            return new ResponseModel()
            {
                Status = 1,
                Message = "Update bank account successful!"
            };
        }
    }
}
