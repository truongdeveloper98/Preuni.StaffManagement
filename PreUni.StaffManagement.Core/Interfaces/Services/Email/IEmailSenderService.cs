using PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Email;

namespace PreUni.StaffManagement.Core.Interfaces.Services.Email
{
	public interface IEmailSenderService
	{
		void SendEmail(Message message);
	}
}
