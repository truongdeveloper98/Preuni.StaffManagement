using PreUni.StaffManagement.Core.Constants.Interviews;
using PreUni.StaffManagement.Core.Interfaces.Repositories.Interviews;
using PreUni.StaffManagement.Core.Interfaces.Services.Interviews;
using PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Interviews;
using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels;
using PreUni.StaffManagement.Domain.Entities;
using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels.Interviews;
using PreUni.StaffManagement.Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace PreUni.StaffManagement.Infrastructure.Services.Interviews
{
	public class TFNDeclareService : ITFNDeclareService
    {
        private readonly ITFNDeclareRepository _tfnDeclareRepository;

        public TFNDeclareService(ITFNDeclareRepository tfnDeclareRepository)
        {
            _tfnDeclareRepository = tfnDeclareRepository;
        }

        /// <summary>
        /// Method create TFN Declare
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>`````
        
        public async Task<ResponseModel<TFNDeclare>> GetTFNDeclareByJobApplicationId(int jobApplicationId)
        {
            var tfnDeclare = await _tfnDeclareRepository
                .Query()
                .Select(i => new TFNDeclare
                {
                    TaxNumber = i.TaxNumber,
                    NameTitle = i.NameTitle,
                    Surname = i.Surname,
                    FirstName = i.FirstName,
                    OtherName = i.OtherName,
                    PreviousLastName = i.PreviousLastName,
                    Address = i.Address,
                    Suburb = i.Suburb,
                    State = i.State,
                    PostCode = i.PostCode,
                    WorkType = i.WorkType,
                    IsAustraliaResident = i.IsAustraliaResident,
                    IsTaxFee = i.IsTaxFee,
                    IsTaxOffsetForPensioners = i.IsTaxOffsetForPensioners,
                    IsTaxOffsetForCarer = i.IsTaxOffsetForCarer,
                    IsEducationLoan = i.IsEducationLoan,
                    IsFinancialSupplementSupport = i.IsFinancialSupplementSupport,
                    TaxNumberOption = i.TaxNumberOption,
                    Signature = i.Signature,
                    SignDate = i.SignDate,
                })
                .FirstOrDefaultAsync(i => i.JobApplicationId == jobApplicationId);

            return new ResponseModel<TFNDeclare>
            {
                Status = 1,
                Message = "success",
                Data = tfnDeclare
            };
        }
        
        public async Task<ResponseModel> Create(TFNDeclare request)
        {
            
            // Save data into Database
            await _tfnDeclareRepository.Add(request);

            // return result
            return new ResponseModel()
            {
                Status = 1,
                Message = TFNDeclareConstant.CreateSuccessed
            };
        }

		public async Task<ResponseModel<TFNDeclare>> GetByJobId(int Id)
		{
			var result = await _tfnDeclareRepository.GetByJobApplicationId(Id);
            return result;
		}
    
        public async Task<ResponseModel> Update(TFNDeclare model, int jobApplicationId)
        {
            var tfnDeclare = await _tfnDeclareRepository
                .Query()
                .FirstOrDefaultAsync(i => i.JobApplicationId == jobApplicationId);

            if (tfnDeclare == default)
            {
                throw new Exception("Cannot find TFN Declare.");
            }

            tfnDeclare.TaxNumber = model.TaxNumber;
            tfnDeclare.TaxNumberOption = model.TaxNumberOption;
            tfnDeclare.NameTitle = model.NameTitle;
            tfnDeclare.Surname = model.Surname;
            tfnDeclare.OtherName = model.OtherName;
            tfnDeclare.PreviousLastName = model.PreviousLastName;
            tfnDeclare.Address = model.Address;
            tfnDeclare.Suburb = model.Suburb;
            tfnDeclare.State = model.State;
            tfnDeclare.WorkType = model.WorkType;
            tfnDeclare.IsAustraliaResident = model.IsAustraliaResident;
            tfnDeclare.IsTaxFee = model.IsTaxFee;
            tfnDeclare.IsTaxOffsetForPensioners = model.IsTaxOffsetForPensioners;
            tfnDeclare.IsTaxOffsetForCarer = model.IsTaxOffsetForCarer;
            tfnDeclare.IsEducationLoan = model.IsEducationLoan;
            tfnDeclare.IsFinancialSupplementSupport = model.IsFinancialSupplementSupport;
            tfnDeclare.DayOfBirth = model.DayOfBirth;
            tfnDeclare.MonthOfBirth = model.MonthOfBirth;
            tfnDeclare.YearOfBirth = model.YearOfBirth;
            tfnDeclare.SignDate = model.SignDate;
            tfnDeclare.Signature = model.Signature;

            await _tfnDeclareRepository.Update(tfnDeclare);

            return new ResponseModel()
            {
                Status = 1,
                Message = "Update TFN Declare successful!"
            };
        }
    }
}
