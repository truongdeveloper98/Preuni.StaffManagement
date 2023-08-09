using PreUni.StaffManagement.Domain.Enum;

namespace PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Interviews
{
	public class JobApplicationRequestModel
	{
        public string JobTitle { get; set; } = default!;
		public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? PhoneNumber { get; set; }
        public JobApplicationStatus Status { get; set; } = JobApplicationStatus.New;
        public DateTimeOffset AppliedDate { get; set; }
	}
}
