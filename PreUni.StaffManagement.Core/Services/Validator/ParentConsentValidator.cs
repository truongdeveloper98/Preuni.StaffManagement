using FluentValidation;
using PreUni.StaffManagement.Core.ViewModels.ValidateViewModels;
using PreUni.StaffManagement.Domain.Entities;

namespace PreUni.StaffManagement.Core.Services.Validator
{
    public class ParentConsentValidator : AbstractValidator<ParentConsent>
    {
        public ParentConsentValidator() 
        {
            RuleFor(x => x.ContactNameFirst).NotEmpty().WithMessage("Contact Name 1 is required");
            RuleFor(x => x.ContactNameNumberFirst).NotEmpty().WithMessage("Contact Name Number 1 is required");
            RuleFor(x => x.ContactNameSecond).NotEmpty().WithMessage("Contact Name 2 is required");
            RuleFor(x => x.ContactNameNumberSecond).NotEmpty().WithMessage("Contact Name Number 2 is required");
            RuleFor(x => x.ParentName).NotEmpty().WithMessage("Parent Name is required");
            RuleFor(x => x.JobApplicationId).NotEmpty().WithMessage("JobApplication Id is required");
            RuleFor(x => x.RelationshipApplicantFirst).NotEmpty().WithMessage("Relationship to Applicant is requird");
            RuleFor(x => x.RelationshipApplicantSecond).NotEmpty().WithMessage("Relationship to Applicant is requird");
            RuleFor(x => x.ApplicantName).NotEmpty().WithMessage("Name Applicant is requied");
        }
    }
}
