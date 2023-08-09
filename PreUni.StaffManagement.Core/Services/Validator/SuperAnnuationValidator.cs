using FluentValidation;
using PreUni.StaffManagement.Core.ViewModels.ValidateViewModels;

namespace PreUni.StaffManagement.Core.Services.Validator
{
    public class SuperAnnuationSmfsDetailsValidator : AbstractValidator<ValidateSmfsDetails>
    {
        public SuperAnnuationSmfsDetailsValidator()
        {
            RuleFor(x => x.SuperAnnuationType).NotEmpty().WithMessage("SuperAnnuationType is required");
            RuleFor(x => x.JobApplicationId).NotEmpty().WithMessage("JobApplicationId required");
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Full Name is required");
            RuleFor(x => x.EmployeeNumber).NotEmpty().WithMessage("Employee identification number required");
            RuleFor(x => x.SmsfName).NotEmpty().WithMessage("SMSF name is required");
            RuleFor(x => x.SmsfNumber).NotEmpty().WithMessage("SMSF Australian business number is required");
            RuleFor(x => x.SmsfAppearAccount).NotEmpty().WithMessage("Your full name as it appears on your account is required");
            RuleFor(x => x.SmsfServiceAddress).NotEmpty().WithMessage("SMSF electronic service address is required");
            RuleFor(x => x.BankAccountName).NotEmpty().WithMessage("Bank account name is required");
            RuleFor(x => x.BsbCode).NotEmpty().WithMessage("BSBcod is required");
            RuleFor(x => x.SmsfAcountNumber).NotEmpty().WithMessage("Account number is required");
            RuleFor(x => x.HaveAtoSmsf).NotEmpty().WithMessage("This is required");
            RuleFor(x => x.SignatureSmsf).NotEmpty().WithMessage("Signature is required");
            RuleFor(x => x.DateSignatureSmsf).NotEmpty().WithMessage("Date is required");
        }
    }

    public class SuperAnnuationBusinessDetailsValidator : AbstractValidator<ValidateBusinessDetails>
    {
        public SuperAnnuationBusinessDetailsValidator()
        {
            RuleFor(x => x.SuperAnnuationType).NotEmpty().WithMessage("SuperAnnuationType is required");
            RuleFor(x => x.JobApplicationId).NotEmpty().WithMessage("JobApplicationId required");
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Full Name is required");
            RuleFor(x => x.EmployeeNumber).NotEmpty().WithMessage("Employee identification number required");
            RuleFor(x => x.BusinessName).NotEmpty().WithMessage("Business name is required");
            RuleFor(x => x.BusinessNumber).NotEmpty().WithMessage("Australian business number is required");
            RuleFor(x => x.BusinessFundName).NotEmpty().WithMessage("Super fund name is required");
            RuleFor(x => x.BusinessFundNumber).NotEmpty().WithMessage("Super fund Australian business number is required");
            RuleFor(x => x.BusinessAnnuationIdentifi).NotEmpty().WithMessage("Unique superannuation identifier  is required");
            RuleFor(x => x.SignatureEmloyee).NotEmpty().WithMessage("Signature  is required");
            RuleFor(x => x.DateSignatureEmloyee).NotEmpty().WithMessage("Date is required");
        }
    }

    public class SuperAnnuationSuperFundDetailsValidator : AbstractValidator<ValidateSuperFundDetails>
    {
        public SuperAnnuationSuperFundDetailsValidator()
        {
            RuleFor(x => x.SuperAnnuationType).NotEmpty().WithMessage("SuperAnnuationType is required");
            RuleFor(x => x.JobApplicationId).NotEmpty().WithMessage("JobApplicationId required");
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Full Name is required");
            RuleFor(x => x.EmployeeNumber).NotEmpty().WithMessage("Employee identification number required");
            RuleFor(x => x.FundName).NotEmpty().WithMessage("Super Fund name is required");
            RuleFor(x => x.FundBusinessNumber).NotEmpty().WithMessage("Super fund Australian business number (ABN) required");
            RuleFor(x => x.FundAnnuationIdentifi).NotEmpty().WithMessage("Unique superannuation identifier is required");
            RuleFor(x => x.FundAccountNumber).NotEmpty().WithMessage("Your member account number");
            RuleFor(x => x.FundAppearName).NotEmpty().WithMessage("Your name as it appears on your account is required");
            RuleFor(x => x.FundSignature).NotEmpty().WithMessage("Signature  is required");
            RuleFor(x => x.DateFundSignature).NotEmpty().WithMessage("Date is required");
        }
    }
    
    public class SuperAnnuationYourDetailsValidator : AbstractValidator<ValidateYourDetails>
    {
        public SuperAnnuationYourDetailsValidator()
        {
            RuleFor(x => x.JobApplicationId).NotEmpty().WithMessage("JobApplicationId required");
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Full Name is required");
            RuleFor(x => x.EmployeeNumber).NotEmpty().WithMessage("Employee identification number is required").Matches("^[0-9]").WithMessage("Employee identification number is not in the correct format")
                .Length(16).WithMessage("Employee identification number 16 digits");
            RuleFor(x => x.SuperAnnuationType).NotEmpty()
                .WithMessage("You need to choose your super to be paid into ...");
        }
    }
}
