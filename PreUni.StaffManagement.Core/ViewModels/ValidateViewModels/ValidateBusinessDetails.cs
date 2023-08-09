namespace PreUni.StaffManagement.Core.ViewModels.ValidateViewModels
{
    public class ValidateBusinessDetails
    {
        public string? SuperAnnuationType { get; set; }

        public int? JobApplicationId { get; set; }

        //Section A
        public string? FullName { get; set; }
        public string? EmployeeNumber { get; set; }
        public string? TaxFileNumber { get; set; }
        //Section C
        public string? BusinessName { get; set; }
        public string? BusinessNumber { get; set; }
        public string? BusinessFundName { get; set; }
        public string? BusinessFundNumber { get; set; }
        public string? BusinessAnnuationIdentifi { get; set; }
        public bool? BusinessNewAccount { get; set; }
        public string? SignatureEmloyee { get; set; }
        public DateTime? DateSignatureEmloyee { get; set; }
    }
}
