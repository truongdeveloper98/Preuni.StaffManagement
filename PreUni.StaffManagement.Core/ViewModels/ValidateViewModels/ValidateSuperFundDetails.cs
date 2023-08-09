namespace PreUni.StaffManagement.Core.ViewModels.ValidateViewModels
{
    public class ValidateSuperFundDetails
    {
        public string? SuperAnnuationType { get; set; }

        public int? JobApplicationId { get; set; }

        //Section A
        public string? FullName { get; set; }
        public string? EmployeeNumber { get; set; }
        public string? TaxFileNumber { get; set; }

        //Section B
        public string? FundName { get; set; }
        public string? FundBusinessNumber { get; set; }
        public string? FundAnnuationIdentifi { get; set; }
        public string? FundAccountNumber { get; set; }
        public string? FundAppearName { get; set; }
        public bool? FundAttachLetter { get; set; }
        public string? FundSignature { get; set; }
        public DateTime? DateFundSignature { get; set; }
    }
}
