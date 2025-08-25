using EmployeeAdminPortalLST.Data;
using EmployeeAdminPortalLST.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdminPortalLST.Controllers
{
    //localhost:xxxx/api/employees
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public EmployeeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            
        }

        [HttpGet]
        public IActionResult GetAllEmployess()
        {
            //var AllEmployess = dbContext.Employees.ToList();
            return Ok(dbContext.Employees.ToList());

        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetEmployessById(Guid id)
        {

            var employee = dbContext.Employees.Find(id);
            if (employee is null)
            {
                return NotFound();
            }
            return Ok(new
            {
                Message = "$Emoloyee Find Successfully: ",
                Data = employee
            }); 
        
        }



       [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            var employeeEntity = new Employee()
            { 
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary

            };

            dbContext.Employees.Add(employeeEntity);
            dbContext.SaveChanges();
            return Ok(employeeEntity);
        }


        [HttpPut]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] UpdateEmployeeDto dto)
        {
            var employee = await dbContext.Employees.FindAsync(id);
            if (employee == null) return NotFound();

            employee.Name = dto.Name;
            employee.Email = dto.Email;
            employee.Phone = dto.Phone;
            employee.Salary = dto.Salary;

            await dbContext.SaveChangesAsync();
            return Ok(employee);
        }


        [HttpDelete]
        [Route("{id:Guid}")]

        public  IActionResult DeleteEmployee(Guid id)
        {
            var employee =  dbContext.Employees.Find(id);
            if (employee is null) return NotFound();
            dbContext.Employees.Remove(employee);
            dbContext.Employees.Remove(employee);
             dbContext.SaveChangesAsync();

            return Ok(employee);
        }

    }
}
