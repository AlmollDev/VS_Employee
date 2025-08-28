namespace EmployeeAdminPortalLST.Models.Filters
{
    public class _EmployeeFilterRequsetModel
    {
        public string? Name { get; set; }
        public string? Email {  get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
        //paging
        public int PageNumebr { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; } = "Name"; // پیش فرض سورت کردنمون
    }
}
