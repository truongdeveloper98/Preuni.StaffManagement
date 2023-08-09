namespace PreUni.StaffManagement.Domain.Entities;

public class SuperAnnuation : BaseEntity
{
    public string? SuperAnnuationType { get; set; } 

    public int? JobApplicationId { get; set; }
    public JobApplication? JobApplication { get; set; } 
    
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
    
    //Section C
    public string? BusinessName { get; set; } 
    public string? BusinessNumber { get; set; }
    public string? BusinessFundName { get; set; } 
    public string? BusinessFundNumber { get; set; }
    public string? BusinessAnnuationIdentifi { get; set; }
    public bool? BusinessNewAccount { get; set; }
    public string? SignatureEmloyee { get; set; }
    public DateTime? DateSignatureEmloyee { get; set; } 
    
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