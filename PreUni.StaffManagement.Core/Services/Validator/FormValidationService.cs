using FluentValidation.Results;
using FluentValidation;
using PreUni.StaffManagement.Core.ViewModels.ValidateViewModels;
using PreUni.StaffManagement.Domain.Entities;

namespace PreUni.StaffManagement.Core.Services.Validator
{
    public class FormValidationService
    {
        public async Task<ValidationResult> ValidateSuperFundDetails(ValidateSuperFundDetails form)
        {
            var validator = new SuperAnnuationSuperFundDetailsValidator();
            return await validator.ValidateAsync(form);
        }
        public async Task<ValidationResult> ValidateBusinessDetails(ValidateBusinessDetails form)
        {
            var validator = new SuperAnnuationBusinessDetailsValidator();
            return await validator.ValidateAsync(form);
        }
        public async Task<ValidationResult> ValidateSmfsDetails(ValidateSmfsDetails form)
        {
            var validator = new SuperAnnuationSmfsDetailsValidator();
            return await validator.ValidateAsync(form);
        }
        public async Task<ValidationResult> ValidateYourDetails(ValidateYourDetails form)
        {
            var validator = new SuperAnnuationYourDetailsValidator();
            return await validator.ValidateAsync(form);
        }
        public async Task<ValidationResult> ValidateTFNDeclare(TFNDeclare form)
        {
            var validator = new TFNDeclareValidator();
            return await validator.ValidateAsync(form);
        }
        public async Task<ValidationResult> ValidateBankAccountDetail(BankAccountDetail form)
        {
            var validator = new BankAccountDetailValidator();
            return await validator.ValidateAsync(form);
        }
        public async Task<ValidationResult> ValidateParentConsent(ParentConsent form)
        {
            var validator = new ParentConsentValidator();
            return await validator.ValidateAsync(form);
        }
    }
}
