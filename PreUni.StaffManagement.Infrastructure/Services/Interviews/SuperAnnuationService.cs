using PreUni.StaffManagement.Core.Interfaces;
using PreUni.StaffManagement.Core.Interfaces.Repositories;
using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels;
using PreUni.StaffManagement.Domain.Entities;

namespace PreUni.StaffManagement.Infrastructure.Services.Interviews;

public class SuperAnnuationService : ISuperAnnuationService
{
    private readonly ISuperAnnuationRepository _superAnnuationRepository;

    public SuperAnnuationService(ISuperAnnuationRepository superAnnuationRepository)
    {
        _superAnnuationRepository = superAnnuationRepository;
    }

    public async Task<ResponseModel> Create(SuperAnnuation request)
    {
        await _superAnnuationRepository.Add(request);
        return new ResponseModel()
        {
            Status = 1,
            Message = "Create SuperAnnuation successful"
        };
    }

    public Task<SuperAnnuation> GetOne(int jobId)
    {
        var res = _superAnnuationRepository.Find(x => x.JobApplicationId == jobId).First();
        if (res is null)
        {
            return Task.FromResult(new SuperAnnuation());
        }

        return Task.FromResult(res);
    }

    public async Task<ResponseModel> Update(int id, SuperAnnuation request)
    {
        var superAnnuationType = await _superAnnuationRepository.Get(id);
        if (superAnnuationType is null)
            throw new Exception("Super Annuation is null");

        superAnnuationType.SuperAnnuationType = request.SuperAnnuationType;
        superAnnuationType.FullName = request.FullName;
        superAnnuationType.EmployeeNumber = request.EmployeeNumber;
        superAnnuationType.TaxFileNumber = request.TaxFileNumber;
        superAnnuationType.FundName = request.FundName;
        superAnnuationType.FundBusinessNumber = request.FundBusinessNumber;
        superAnnuationType.FundAnnuationIdentifi = request.FundAnnuationIdentifi;
        superAnnuationType.FundAccountNumber = request.FundAccountNumber;
        superAnnuationType.FundAppearName = request.FundAppearName;
        superAnnuationType.FundAttachLetter = request.FundAttachLetter;
        superAnnuationType.FundSignature = request.FundSignature;
        superAnnuationType.DateFundSignature = request.DateFundSignature;
        superAnnuationType.BusinessName = request.BusinessName;
        superAnnuationType.BusinessNumber = request.BusinessNumber;
        superAnnuationType.BusinessFundName = request.BusinessFundName;
        superAnnuationType.BusinessFundNumber = request.BusinessFundNumber;
        superAnnuationType.BusinessAnnuationIdentifi = request.BusinessAnnuationIdentifi;
        superAnnuationType.BusinessNewAccount = request.BusinessNewAccount;
        superAnnuationType.SignatureEmloyee = request.SignatureEmloyee;
        superAnnuationType.DateSignatureEmloyee = request.DateSignatureEmloyee;
        superAnnuationType.SmsfName = request.SmsfName;
        superAnnuationType.SmsfNumber = request.SmsfNumber;
        superAnnuationType.SmsfAppearAccount = request.SmsfAppearAccount;
        superAnnuationType.SmsfServiceAddress = request.SmsfServiceAddress;
        superAnnuationType.BankAccountName = request.BankAccountName;
        superAnnuationType.BsbCode = request.BsbCode;
        superAnnuationType.SmsfAcountNumber = request.SmsfAcountNumber;
        superAnnuationType.HaveAtoSmsf = request.HaveAtoSmsf;
        superAnnuationType.SignatureSmsf = request.SignatureSmsf;
        superAnnuationType.DateSignatureSmsf = request.DateSignatureSmsf;
        await _superAnnuationRepository.Update(superAnnuationType);
        return new ResponseModel()
        {
            Status = 1,
            Message = "Create SuperAnnuation successful"
        };
    }
}