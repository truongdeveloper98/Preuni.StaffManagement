using FluentValidation;
using PreUni.StaffManagement.Core.ViewModels.ValidateViewModels;
using PreUni.StaffManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreUni.StaffManagement.Core.Services.Validator
{
    public class BankAccountDetailValidator : AbstractValidator<BankAccountDetail>
    {
        public BankAccountDetailValidator()
        {
            RuleFor(x => x.BankName).NotEmpty().WithMessage("Bank Name is required");
            RuleFor(x => x.AccountHolderName).NotEmpty().WithMessage("Account Holder Name is required");
            RuleFor(x => x.BSBNumber).NotEmpty().WithMessage("BSBNumber is required");
            RuleFor(x => x.AccountNumber).NotEmpty().WithMessage("AccountNumber is required");
            RuleFor(x => x.EmailAddress).NotEmpty().EmailAddress().WithMessage("EmailAddress is required");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("PhoneNumber is required");
            RuleFor(x => x.JobApplicationId).NotEmpty().WithMessage("JobApplication Id is required");
            RuleFor(x => x.NameApplicant).NotEmpty().WithMessage("Name Applicant is required");
            RuleFor(x => x.Signature).NotEmpty().WithMessage("Signature is required");
            RuleFor(x => x.SignDate).NotEmpty().WithMessage("Sign date is required");
        }
    }
}
