namespace EmployeeAdminPortalLST.Models.Entities
{
    public abstract class EmployeeModel
    {
        public abstract required string Name { get; set; }
        public abstract required string Email { get; set; }
        public abstract string? Phone { get; set; }
        public abstract decimal Salary { get; set; }
    }
}
