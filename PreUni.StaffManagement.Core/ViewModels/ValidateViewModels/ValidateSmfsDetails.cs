namespace PreUni.StaffManagement.Core.ViewModels.ValidateViewModels
{
    public class ValidateSmfsDetails
    {
        public string? SuperAnnuationType { get; set; }

        public int? JobApplicationId { get; set; }

        //Section A
        public string? FullName { get; set; }
        public string? EmployeeNumber { get; set; }
        public string? TaxFileNumber { get; set; }

        //Section D
        public string? SmsfName { get; set; }
        public string? SmsfNumber { get; set; }
        public string? SmsfAppearAccount { get; set; }
        public string? SmsfServiceAddress { get; set; }
        public string? BankAccountName { get; set; }
        public string? BsbCode { get; set; }
        public string? SmsfAcountNumber { get; set; }
        public bool? HaveAtoSmsf { get; set; }
        public string? SignatureSmsf { get; set; }
        public DateTime? DateSignatureSmsf { get; set; }
    }
}
