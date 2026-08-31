using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sampleapi.Data;
using sampleapi.Models;

namespace sampleapi.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }
        //Get all details
        [HttpGet]
        public async Task<IActionResult> GetEmployees(int page = 1, int pageSize = 10, string? department = null, string? sortBy = null , string? search = null)
        {
            if(page < 1)
            {
                return BadRequest(" Page must be greater than 0");
            }
            if(pageSize < 1 || pageSize > 100)
            {
                return BadRequest(" Page size must be between 1 and 100");
            }

            var employees = _context.detail.AsQueryable();
            if (!string.IsNullOrEmpty(department))
            {
                employees = employees.Where(x => x.Department == department);
            }
            if(!string.IsNullOrEmpty(search))
            {
                employees = employees.Where(x => x.Name.Contains(search) || x.Position.Contains(search));
            }
            if (sortBy == "Age")
            {
                employees = employees.OrderBy(x => x.Age);
            }
            if (sortBy == "Name")
            {
                employees = employees.OrderBy(x => x.Name);
            }
            var totalRecords = await employees.CountAsync();
            var totalPages = (int)Math.Ceiling(
               (double)totalRecords / pageSize);


            employees = employees.Skip((page - 1) * pageSize);
            employees = employees.Take(pageSize);

            var result = await employees.ToListAsync();

            return Ok(new
            {
                page , pageSize , totalRecords , totalPages , data = result
            });
        }

        //Get specific Id's person details
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetEmployee(int Id)
        {
            var details = await _context.detail.FindAsync(Id);

            if (details == null)
                return NotFound();

            return Ok(details);
        }
        //Post the person details
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(SampleModel sample)
        {
            _context.detail.Add(sample);
            await _context.SaveChangesAsync();
            return Ok(sample);
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateEmployee(int Id, SampleModel sample)
        {
            var findEmployee = await _context.detail.FindAsync(Id);
            if(findEmployee == null)
            {
                return NotFound();
            }
            findEmployee.Name = sample.Name;
            findEmployee.Age = sample.Age;
            findEmployee.Position = sample.Position;
            findEmployee.Department = sample.Department;
            await _context.SaveChangesAsync();
            return Ok(findEmployee);
        }
       
    }

}
