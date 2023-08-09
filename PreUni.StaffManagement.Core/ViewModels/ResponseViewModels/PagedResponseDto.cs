
namespace PreUni.StaffManagement.Core.ViewModels.ResponseViewModels
{
    public class PagedResponseDto<T>
    {
        public int Page { get; set; }
        public int Limit { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public IList<T> Items { get; set; }
        public PagedResponseDto()
        {
            Items = new List<T>();
        }
    }
}