using PreUni.StaffManagement.Domain.Entities;
using PreUni.StaffManagement.Domain.Enum;

namespace PreUni.StaffManagement.Core.ViewModels.ResponseViewModels.Interviews
{
    public class TFNDeclareResponseViewModel : BaseEntity
    {
        public string FaxNumber { get; set; } = default!;
        public string NameTitle { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string FirstName { get; set; } = default!;

        public string OtherName { get; set; } = default!;

        public string PreviousLastName { get; set; } = default!;

        public string Address { get; set; } = default!;
        public string Suburb { get; set; } = default!;
        public string State { get; set; } = default!;

        public string PostCode { get; set; } = default!;

        public WorkType WorkType { get; set; }

        public bool IsAustraliaResident { get; set; }

        public bool IsTaxFee { get; set; }

        public bool IsTaxOffsetForPensioners { get; set; }

        public bool IsTaxOffsetForCarer { get; set; }

        public bool IsEducationLoan { get; set; }

        public bool IsFinancialSupplementSupport { get; set; }

        public JobApplication JobApplication { get; set; } = default!;

        public int JobApplicationId { get; set; }
    }
}
