namespace EmployeeAdminPortalLST.Models.Entities
{
    public class AddEmployeeDto(AddEmployeeDto Employee) : EmployeeModel
    {
        public override required string Name { get; set; } = Employee.Name;
        public override required string Email { get; set; } = Employee.Email;
        public override string? Phone { get; set; } = Employee.Phone;
        public override decimal Salary { get; set; } = Employee.Salary;
    }
}
