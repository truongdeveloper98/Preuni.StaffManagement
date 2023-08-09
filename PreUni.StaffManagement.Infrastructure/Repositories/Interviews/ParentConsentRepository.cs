using Microsoft.EntityFrameworkCore;
using PreUni.StaffManagement.Core.Interfaces.Repositories.Interviews;
using PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Interviews;
using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels;
using PreUni.StaffManagement.Domain.Entities;
using PreUni.StaffManagement.Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreUni.StaffManagement.Infrastructure.Repositories.Interviews
{
	public class ParentConsentRepository : RepositoryBase<ParentConsent>, IParentConsentRepository
	{
		public ParentConsentRepository(ApplicationDbContext context) : base(context)
		{
		}

		public async Task<ResponseModel<ParentConsent>> GetByJobApplicationId(int id)
		{
			var result = await _context.ParentConsents.Select(x => new ParentConsent()
			{
				Id = x.Id,
				ContactNameFirst = x.ContactNameFirst,
				ParentName = x.ParentName,
				ContactNameNumberFirst = x.ContactNameNumberFirst,
				ContactNameNumberSecond = x.ContactNameNumberSecond,
				ContactNameSecond = x.ContactNameSecond,
				JobApplicationId = x.JobApplicationId,
				RelationshipApplicantFirst = x.RelationshipApplicantFirst,
				RelationshipApplicantSecond = x.RelationshipApplicantSecond,
				Signature = x.Signature,
				SignDate = x.SignDate,
				ApplicantName = x.ApplicantName,

			}).FirstOrDefaultAsync(x=> x.JobApplicationId == id);

			if(result is null)
			{
				return new ResponseModel<ParentConsent>()
				{
					Status = 0,
					Message = "Parent Consent Not Found",
					Data = null
				};
			}

			return new ResponseModel<ParentConsent>()
			{
				Status = 1,
				Message = "Get Parent Consent Detail Success",
				Data = result
			};
		}
	}
}
