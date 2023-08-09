using PreUni.StaffManagement.Domain.Enum;

namespace PreUni.StaffManagement.Core.ViewModels.ResponseViewModels.JobApplications
{
	public class JobApplicationResponse
	{
		public int Id { get; set; }
		public string JobTitle { get; set; } = default!;
		public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
		public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public JobApplicationStatus  Status { get; set; } = JobApplicationStatus.New;
        public DateTimeOffset AppliedDate { get; set; }
	}
}
