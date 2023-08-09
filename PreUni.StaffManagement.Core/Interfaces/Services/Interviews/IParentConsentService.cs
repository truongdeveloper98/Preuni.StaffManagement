using PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Interviews;
using PreUni.StaffManagement.Core.ViewModels.ResponseViewModels;
using PreUni.StaffManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreUni.StaffManagement.Core.Interfaces.Services.Interviews
{
	public interface IParentConsentService
	{
		Task<ResponseModel> Create(ParentConsent request);

		Task<ResponseModel<ParentConsent>> GetByJobId(int id);
		Task<ResponseModel> Update(ParentConsent request,int jobApplicationId);
	}
}
