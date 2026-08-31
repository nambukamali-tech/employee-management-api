using Microsoft.AspNetCore.Mvc;
using sampleapi.Data;
using sampleapi.Interfaces;
using sampleapi.Models;

namespace sampleapi.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        //Get all details
        [HttpGet]
        public async Task<IActionResult> GetEmployees(int page = 1 , int pageSize = 10,string? department = null,string? sortBy = null,string? search = null)
        {
            if (page < 1)
            {
                return BadRequest("Page must be greater than 0");
            }

            if (pageSize < 1 || pageSize > 100)
            {
                return BadRequest("Page size must be between 1 and 100");
            }

            var result = await _employeeService.GetEmployees(
                page,
                pageSize,
                department,
                sortBy,
                search);

            return Ok(result);
        }

        //Get specific Id's person details
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetEmployee(int Id)
        {
            var employee = await _employeeService.GetEmployee(Id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        //Post the person details
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(SampleModel sample)
        {
            var employee = await _employeeService.CreateEmployee(sample);
            return Ok(employee);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateEmployee(int Id, SampleModel sample)
        {
            var employee = await _employeeService.UpdateEmployee(Id, sample);
            if (employee == null)
                return NotFound();
            return Ok(employee);
        }
       
    }

}
