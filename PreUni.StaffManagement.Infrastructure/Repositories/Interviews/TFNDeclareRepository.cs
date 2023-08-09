using Microsoft.EntityFrameworkCore;
using PreUni.StaffManagement.Core.Interfaces.Repositories.Interviews;
using PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Interviews;
using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels;
using PreUni.StaffManagement.Domain.Entities;
using PreUni.StaffManagement.Infrastructure.EntityFramework;

namespace PreUni.StaffManagement.Infrastructure.Repositories.Interviews
{
    public class TFNDeclareRepository : RepositoryBase<TFNDeclare>, ITFNDeclareRepository
    {
        public TFNDeclareRepository(ApplicationDbContext context) : base(context)
        {

        }

		public async Task<ResponseModel<TFNDeclare>> GetByJobApplicationId(int id)
		{
			var result = await _context.TFNDeclares.Select(x => new TFNDeclare()
			{
				Id = x.Id,
				Address = x.Address,
				JobApplicationId = x.JobApplicationId,
				TaxNumber = x.TaxNumber,
				FirstName = x.FirstName,
				IsAustraliaResident = x.IsAustraliaResident,
				IsEducationLoan = x.IsEducationLoan,
				IsFinancialSupplementSupport = x.IsFinancialSupplementSupport,
				IsTaxOffsetForCarer = x.IsTaxOffsetForCarer,
				IsTaxOffsetForPensioners = x.IsTaxOffsetForPensioners,
				NameTitle = x.NameTitle,
				OtherName = x.OtherName,
				IsTaxFee = x.IsTaxFee,
				PreviousLastName = x.PreviousLastName,
				PostCode = x.PostCode,
				Suburb = x.Suburb,
				State = x.State,
				WorkType = x.WorkType,
				Surname = x.Surname,
				MonthOfBirth = x.MonthOfBirth,
				YearOfBirth = x.YearOfBirth,
				DayOfBirth = x.DayOfBirth,
				TaxNumberOption = x.TaxNumberOption,
				SignDate = x.SignDate,
				Signature = x.Signature,
			}).FirstOrDefaultAsync(x => x.JobApplicationId == id);

			if(result is null)
			{
				return new ResponseModel<TFNDeclare>()
				{
					Status = 0,
					Message = "No Data",
					Data = null
				};
			}

			return new ResponseModel<TFNDeclare>()
			{
				Status = 1,
				Message = "Get Data Success",
				Data = result
			};
		}
	}
}
