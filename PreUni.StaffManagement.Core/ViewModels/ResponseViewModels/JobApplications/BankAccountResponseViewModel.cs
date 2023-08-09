namespace PreUni.StaffManagement.Core.ViewModels.ResponseViewModels.Interviews
{
    public class BankAccountResponseViewModel
    {
        public string BankName { get; set; } = default!;
        public string AccountHolderName { get; set; } = default!;
        public int BSBNumber { get; set; }
        public int AccountNumber { get; set; }
        public string EmailAddress { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public int JobApplicationId { get; set; }
    }
}
