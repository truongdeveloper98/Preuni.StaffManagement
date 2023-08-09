
namespace PreUni.StaffManagement.Domain.Entities
{
	public class ParentConsent : BaseEntity
	{
		public string ContactNameFirst { get; set; } = default!;
		public string ContactNameNumberFirst { get; set; } = default!;
		public string RelationshipApplicantFirst { get; set; } = default!;
		public string ContactNameSecond { get; set; } = default!;
		public string ContactNameNumberSecond { get; set; } = default!;
        public string RelationshipApplicantSecond { get; set; } = default!;
        public string ParentName { get; set; } = default!;
		public string ApplicantName { get; set; } = default!;
        public DateTime SignDate { get; set; } = default!;
        public string Signature { get; set; } = default!;
        public JobApplication JobApplication { get; set; } = default!;
		public int JobApplicationId { get; set; }


	}
}
