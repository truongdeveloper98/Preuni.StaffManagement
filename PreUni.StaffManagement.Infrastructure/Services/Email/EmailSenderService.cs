using MimeKit;
using MailKit.Net.Smtp;
using PreUni.StaffManagement.Core.Configuration;
using PreUni.StaffManagement.Core.Interfaces.Services.Email;
using PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Email;

namespace PreUni.StaffManagement.Infrastructure.Services.Email
{
	public class EmailSenderService : IEmailSenderService
	{
		private readonly EmailConfiguration _emailConfig;
		public EmailSenderService(EmailConfiguration emailConfiguration) 
		{
			_emailConfig = emailConfiguration;
		}


		public void SendEmail(Message message)
		{
			var emailMessage = CreateEmailMessage(message);
			Send(emailMessage);
		}

		private MimeMessage CreateEmailMessage(Message message)
		{
			var emailMessage = new MimeMessage();
			emailMessage.From.Add(new MailboxAddress(_emailConfig.From, _emailConfig.From));
			emailMessage.To.AddRange(message.To);
			emailMessage.Subject = message.Subject;

			var bodyBuilder = new BodyBuilder { HtmlBody = message.Content };
			emailMessage.Body = bodyBuilder.ToMessageBody();

			//emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

			return emailMessage;
		}

		private void Send(MimeMessage mailMessage)
		{
			using var client = new SmtpClient();
			try
			{
				client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
				client.AuthenticationMechanisms.Remove("XOAUTH2");
				client.Authenticate(_emailConfig.UserName, _emailConfig.Password);

				client.Send(mailMessage);
			}
			catch
			{
				//log an error message or throw an exception or both.
				throw;
			}
			finally
			{
				client.Disconnect(true);
				client.Dispose();
			}
		}
	}
}
