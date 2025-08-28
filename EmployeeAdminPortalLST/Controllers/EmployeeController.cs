using EmployeeAdminPortalLST.Models.DTOs;
using EmployeeAdminPortalLST.Models.Entities;
using EmployeeAdminPortalLST.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdminPortalLST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase // حذف TKey از کلاس
    {
        private readonly IGenericService<Employee, Guid> _employeeService; // استفاده از Guid

        public EmployeeController(IGenericService<Employee, Guid> employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var allEmployees = await _employeeService.GetAllAsync();
            return Ok(allEmployees);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetEmployeeById(Guid id) // تغییر به Guid
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee is null)
            {
                return NotFound(new { Message = "Employee not found" });
            }
            return Ok(new
            {
                Message = "Employee found successfully",
                Data = employee
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] AddEmployeeDto addEmployeeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var employeeEntity = new Employee()
                {
                    id = Guid.NewGuid(),
                    Name = addEmployeeDto.Name,
                    Email = addEmployeeDto.Email,
                    Phone = addEmployeeDto.Phone,
                    Salary = addEmployeeDto.Salary
                };

                await _employeeService.AddAsync(employeeEntity);
                return CreatedAtAction(nameof(GetEmployeeById),
                    new { id = employeeEntity.id }, employeeEntity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Internal Server Error",
                    Details = ex.Message
                });
            }
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] UpdateEmployeeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null)
                return NotFound();

            employee.Name = dto.Name;
            employee.Email = dto.Email;
            employee.Phone = dto.Phone;
            employee.Salary = dto.Salary;

            await _employeeService.UpdateAsync(employee);
            return Ok(employee);
        }

        //[HttpPatch("{id:Guid}")]
        //public async Task<IActionResult> PartialUpdateEmployee(Guid id,
        //    [FromBody] JsonPatchDocument<Employee> patchDoc)
        //{
        //    if (patchDoc == null)
        //        return BadRequest();

        //    var employee = await _employeeService.GetByIdAsync(id);
        //    if (employee == null)
        //        return NotFound();

        //    patchDoc.ApplyTo(employee, ModelState);

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    await _employeeService.UpdateAsync(employee);
        //    return Ok(employee);
        //}

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null)
                return NotFound();

            await _employeeService.DeleteAsync(employee);
            return Ok(new { Message = "Employee deleted successfully" });
        }

        [HttpDelete("bulk")]
        public async Task<IActionResult> DeleteEmployeesBulk([FromBody] BulkDeleteDto request)
        {
            var employees = await _employeeService.AsQueryable()
                .Where(e => request.EmployeeIds.Contains(e.id))
                .ToListAsync();

            if (!employees.Any())
                return NotFound(new { Message = "No employees found to delete." });

            await _employeeService.DeleteRangeAsync(employees);
            return Ok(new
            {
                DeletedCount = employees.Count,
                Message = "Employees deleted successfully."
            });
        }
    }
}