namespace PreUni.StaffManagement.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public int IdentityId { get; set; }
        public bool IsActive { get; set; } = true;
        public JobApplication JobApplication { get; set; } = default!;

        public string? Image { get; set; }
    }
}