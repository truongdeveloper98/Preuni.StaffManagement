namespace PreUni.StaffManagement.Core.ViewModels.RequestViewModels.Interviews
{
    public class JobApplicationListRequestViewModel
    {
        public string sortColumnName { get; set; } = "FirstName";
        public string? textSearch { get; set; } = default!;
        public int currentPage { get; set; }
        public int pageSize { get; set; }
        public string sortColumnDirection { get; set; } = "ASC";
    }
}
