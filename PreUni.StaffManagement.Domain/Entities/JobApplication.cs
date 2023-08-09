using PreUni.StaffManagement.Domain.Enum;

namespace PreUni.StaffManagement.Domain.Entities
{
    public class JobApplication : BaseEntity
    {
        public int UserId { get; set; }

        public string JobTitle { get; set; } = default!;
        public User User { get; set; } = default!;

        public JobApplicationStatus Status { get; set; } = JobApplicationStatus.New;

        public DateTimeOffset AppliedDate { get; set;}

        public TFNDeclare TFNDeclare { get; set; } = default!;

        public BankAccountDetail BankAccountDetail { get; set; } = default!;
        public ParentConsent ParentConsent { get; set; } = default!;
    }
}