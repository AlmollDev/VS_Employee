namespace EmployeeAdminPortalLST.Models.DTOs
{
    public class PageResult<T>
    {
        public int TotalCount { get; set; }      
        public int PageNumber { get; set; }      
        public int PageSize { get; set; }        
        public IEnumerable<T> Data { get; set; } = new List<T>();
    }
}
