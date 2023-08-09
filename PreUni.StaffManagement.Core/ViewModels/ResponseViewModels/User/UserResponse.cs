namespace PreUni.StaffManagement.Core.ViewModels.ResponseViewModels.User
{
	public class UserResponse
	{
		public int Id { get; set; }
		public string FirstName { get; set; } = default!;
		public string LastName { get; set; } = default!;
		public string Email { get; set; } = default!;
		public string PhoneNumber { get; set; } = default!;
		public bool IsActive { get; set; }
		public int? IdentityId { get; set; }
		public List<string> Roles { get; set; } = default!;
	}
}
