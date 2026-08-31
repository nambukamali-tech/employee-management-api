using sampleapi.Data;
using Microsoft.EntityFrameworkCore;
using sampleapi.Interfaces;
using sampleapi.Models;

namespace sampleapi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;
        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<SampleModel?> GetEmployee(int id)
        {
            var employee = await _context.detail.FindAsync(id);
            return employee;
        }
        public async Task<SampleModel?> CreateEmployee(SampleModel sample)
        {
            _context.detail.Add(sample);
            await _context.SaveChangesAsync();
            return sample;
        }
        public async Task<SampleModel?> UpdateEmployee(int id , SampleModel sample)
        {
            var employee = await _context.detail.FindAsync(id);
            if(employee == null)
            {
                return null;
            }
            employee.Name = sample.Name;
            employee.Age = sample.Age;
            employee.Department = sample.Department;
            employee.Position = sample.Position;
            await _context.SaveChangesAsync();
            return employee;
        }
        public async Task<List<SampleModel>> GetEmployees(
    int page,
    int pageSize,
    string? department,
    string? sortBy,
    string? search)
        {
            var employees = _context.detail.AsQueryable();

            if (!string.IsNullOrEmpty(department))
            {
                employees = employees.Where(x => x.Department == department);
            }

            if (!string.IsNullOrEmpty(search))
            {
                employees = employees.Where(x =>
                    x.Name.Contains(search) ||
                    x.Position.Contains(search));
            }

            if (sortBy == "Age")
            {
                employees = employees.OrderBy(x => x.Age);
            }

            if (sortBy == "Name")
            {
                employees = employees.OrderBy(x => x.Name);
            }

            employees = employees
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            return await employees.ToListAsync();
        }
    }
}
