using Microsoft.EntityFrameworkCore;
using PreUni.StaffManagement.Core.Interfaces.Repositories.Interviews;
using PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Interviews;
using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels;
using PreUni.StaffManagement.Domain.Entities;
using PreUni.StaffManagement.Infrastructure.EntityFramework;

namespace PreUni.StaffManagement.Infrastructure.Repositories.Interviews
{
    public class BankAccountRepository : RepositoryBase<BankAccountDetail>, IBankAccountRepository
    {
        public BankAccountRepository(ApplicationDbContext context) : base(context) { }

		public async Task<ResponseModel<BankAccountDetail>> GetByJobApplicationId(int Id)
		{
			var result = await _context.BankAccountDetails.Select(x => new BankAccountDetail()
			{
				Id = x.Id,
				BankName = x.BankName,
				AccountHolderName = x.AccountHolderName,
				BSBNumber = x.BSBNumber,
				PhoneNumber = x.PhoneNumber,
				AccountNumber = x.AccountNumber,
				EmailAddress = x.EmailAddress,
				JobApplicationId = x.JobApplicationId,
				Signature = x.Signature,
				NameApplicant = x.NameApplicant,
				SignDate = x.SignDate,
			}).FirstOrDefaultAsync(x => x.JobApplicationId == Id);

			if(result is null)
			{
				return new ResponseModel<BankAccountDetail>
				{
					Status = 0,
					Message = "Get Bank Account Detail No Data",
					Data = null
				};
			}
			
			return new ResponseModel<BankAccountDetail>
			{
				Status = 1,
				Message = "Get Bank Account Detail Success",
				Data = result
			};
		}
	}
}
