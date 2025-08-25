using Azure;
using EmployeeAdminPortalLST.Data;
using EmployeeAdminPortalLST.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization.Metadata;

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

        //Gets Query
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


        [HttpGet("search")]
        public IActionResult SearchEmlpyees(string? name, string? email, decimal? minSalary, decimal? maxSalary)
        {
            var query = dbContext.Employees.AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.Contains(name));
            }
            if (!string.IsNullOrEmpty(email)) 
            {
                query = query.Where(e=>e.Email.Contains(email));
            }
            if (minSalary.HasValue) 
            {
                query = query.Where(e=>e.Salary >= minSalary.Value);
            }
            if (minSalary.HasValue)
            {
                query = query.Where(e => e.Salary <= maxSalary.Value);
            }

            return Ok(query.ToList());

        }

        [HttpGet("paged")]
        public IActionResult GetPagedEmployees(int pageNumber = 1, int pageSize = 10)
        {
            var employees = dbContext.Employees.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return Ok(employees);


        }

        [HttpGet("sorted")]
        public IActionResult GetSortedEmployees(string sortBy = "Name") 
        {
            var employee = sortBy.ToLower() switch
            {

                "salary" => dbContext.Employees.OrderByDescending(e => e.Salary).ToList(),
                _ => dbContext.Employees.OrderBy(e => e.Name).ToList()
            };
            return Ok(employee);
        }

        [HttpGet("stats")]
        public IActionResult GetEmloyeesStats()
        {
            var TotalEmployee = dbContext.Employees.Count();
            var averrageSalary = dbContext.Employees.Average(e => e.Salary);
            var MaxSalary = dbContext.Employees.Max(e => e.Salary);

            return Ok(new { TotalEmployee, averrageSalary, MaxSalary });
        }

        //patch

   

        //[HttpPatch("{id:Guid}")]
        //public async Task<IActionResult> PartialUpdateEmployee(Guid id, [FromBody] JsonPatchDocument<UpdateEmployeeDto> patchDoc)
        //{
        //    if (patchDoc == null) return BadRequest();

        //    var employee = await dbContext.Employees.FindAsync(id);
        //        if (employee == null) return NotFound();

        //    var employeeToPatch = new UpdateEmployeeDto
        //    {
        //        Name = employee.Name,
        //        Email = employee.Email,
        //        Phone = employee.Phone,
        //        Salary = employee.Salary
        //    };

        //        patchDoc.ApplyTo(employeeToPatch, ModelStateDictionary  ModelState);

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    employee.Name = employeeToPatch.Name;
        //    employee.Email = employeeToPatch.Email;
        //    employee.Phone = employeeToPatch.Phone;
        //    employee.Salary = employeeToPatch.Salary;

        //    await dbContext.SaveChangesAsync();
        //    return Ok(employee);
        //}



        //Post Query
        [HttpPost]
            public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
            {
                try
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
                catch (Exception ex)
                {
                    return StatusCode(500, new { Message = "Internal Server Error", Details = ex.Message });

                }
            }

        //Put Query
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

        //Del querty
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


        [HttpDelete("bulk")]
        public async Task<IActionResult> DeleteEmployeesBulk([FromBody] BulkDeleteDto request)
        {
            try
            {
                var employees = dbContext.Employees.Where(e => request.EmployeeIds.Contains(e.id)).ToList();
                if (!employees.Any()) return NotFound();

                dbContext.Employees.RemoveRange(employees);
                await dbContext.SaveChangesAsync();

                return Ok(new { DeletedCount = employees.Count });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Details = ex.Message });
            }
        }


    }
}
