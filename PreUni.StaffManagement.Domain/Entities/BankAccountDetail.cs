
namespace PreUni.StaffManagement.Domain.Entities
{
    public class BankAccountDetail : BaseEntity
    {
        public string BankName { get; set; } = default!;
        public string AccountHolderName { get; set; } = default!;
        public string BSBNumber { get; set; } = default!;
        public string AccountNumber { get; set; } = default!;
        public string NameApplicant { get; set; } = default!;
        public string EmailAddress { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public DateTime SignDate { get; set; } = default!;
        public string Signature { get; set; } = default!;
        public JobApplication JobApplication { get; set; } = default!;
        public int JobApplicationId { get; set; }
    }
}
