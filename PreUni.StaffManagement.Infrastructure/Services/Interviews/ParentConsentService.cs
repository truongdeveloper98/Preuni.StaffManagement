using Microsoft.EntityFrameworkCore;
using PreUni.StaffManagement.Core.Interfaces.Repositories.Interviews;
using PreUni.StaffManagement.Core.Interfaces.Services.Interviews;
using PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Interviews;
using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels;
using PreUni.StaffManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreUni.StaffManagement.Infrastructure.Services.Interviews
{
    public class ParentConsentService : IParentConsentService
    {
        private readonly IParentConsentRepository _parentConsentRepository;
        public ParentConsentService(IParentConsentRepository parentConsentRepository)
        {
            _parentConsentRepository = parentConsentRepository;
        }

        /// <summary>
        /// Create new Parent Consent
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseModel> Create(ParentConsent request)
        {
            // save parent consent into database
            await _parentConsentRepository.Add(request);

            // return responseModel success
            return new ResponseModel
            {
                Status = 1,
                Message = "Create Parent Consent Successful"
            };

        }

        public async Task<ResponseModel<ParentConsent>> GetByJobId(int id)
        {
            var result = await _parentConsentRepository.GetByJobApplicationId(id);
            return result;
        }

        public async Task<ResponseModel> Update(ParentConsent request, int jobApplicationId)
        {
            var result = await _parentConsentRepository.Query().FirstOrDefaultAsync(x => x.JobApplicationId == jobApplicationId);

            if (result is null)
            {
                throw new Exception("Parent Consent not found");
            }

            result.ContactNameNumberFirst = request.ContactNameNumberFirst;
            result.ContactNameFirst = request.ContactNameFirst;
            result.ContactNameNumberSecond = request.ContactNameNumberSecond;
            result.ContactNameSecond = request.ContactNameSecond;
            result.ParentName = request.ParentName;
            result.ApplicantName = request.ApplicantName;
            result.RelationshipApplicantFirst = request.RelationshipApplicantFirst;
            result.RelationshipApplicantSecond = request.RelationshipApplicantSecond;
            result.SignDate = request.SignDate;
            result.Signature = request.Signature;

            await _parentConsentRepository.Update(result);

            return new ResponseModel
            {
                Status = 1,
                Message = "Update Parent Consent Successful"
            };
        }
    }
}
