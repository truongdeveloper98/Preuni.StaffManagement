using FluentValidation;
using PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Interviews;
using PreUni.StaffManagement.Core.ViewModels.ValidateViewModels;
using PreUni.StaffManagement.Domain.Entities;

namespace PreUni.StaffManagement.Core.Services.Validator
{
    public class TFNDeclareValidator : AbstractValidator<TFNDeclare>
    {
        public TFNDeclareValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Full Name is required");
            RuleFor(x => x.NameTitle).NotEmpty().WithMessage("Name Tilte  is required");
            RuleFor(x => x.TaxNumber).Length(0,11).WithMessage("tax file number not too 9 number");
            RuleFor(x => x.TaxNumberOption).NotEmpty().WithMessage("Fax File Number option is required");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname is required");
			RuleFor(x => x.Surname).Length(0,19).WithMessage("Please enter no more than 19 characters");
			RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required");
			RuleFor(x => x.Address).Length(0, 38).WithMessage("Please enter no more than 38 characters");
			RuleFor(x => x.Suburb).NotEmpty().WithMessage("Suburb is required");
			RuleFor(x => x.Suburb).Length(0, 19).WithMessage("Please enter no more than 19 characters");
			RuleFor(x => x.State).NotEmpty().WithMessage("State is required");
			RuleFor(x => x.State).Length(0, 3).WithMessage("Please enter no more than 3 characters");
			RuleFor(x => x.PostCode).NotEmpty().WithMessage("Postcode is required");
			RuleFor(x => x.PostCode).Length(0, 4).WithMessage("Please enter no more than 4 characters");
			RuleFor(x => x.WorkType).NotEmpty().WithMessage("WorkType is required");
            RuleFor(x => x.IsAustraliaResident).NotNull().WithMessage("Is AustraliaResident is required");
            RuleFor(x => x.IsTaxFee).NotNull().WithMessage("Is TaxFee is required");
            RuleFor(x => x.IsTaxOffsetForPensioners).NotNull().WithMessage("Is TaxOffset For Pensioners is required");
            RuleFor(x => x.IsTaxOffsetForPensioners).NotNull().WithMessage("Is Tax Offset For Pensioners is required");
            RuleFor(x => x.IsEducationLoan).NotNull().WithMessage("Is Education Loan is required");
            RuleFor(x => x.IsFinancialSupplementSupport).NotNull().WithMessage("Is Financial Supplement Support is required");
            RuleFor(x => x.JobApplicationId).NotEmpty().WithMessage("JobApplication Id is required");
            RuleFor(x => x.DayOfBirth).NotEmpty().WithMessage("Day of birth is required");
            RuleFor(x => x.MonthOfBirth).NotEmpty().WithMessage("Month of birth is required");
            RuleFor(x => x.YearOfBirth).NotEmpty().WithMessage("Year of birth is required");
			RuleFor(x => x.Signature).NotEmpty().WithMessage("Signature is required");
            RuleFor(x => x.SignDate).NotEmpty().WithMessage("Sign date is required");
        }
    }
}
