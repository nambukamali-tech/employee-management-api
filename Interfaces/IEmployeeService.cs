using sampleapi.Models;

namespace sampleapi.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<SampleModel>> GetEmployees(int page , int pageSize , string? Department , string? sortBy , string? search );
        Task<SampleModel?> GetEmployee(int Id);
        Task<SampleModel?> CreateEmployee(SampleModel sample);
        Task<SampleModel?> UpdateEmployee(int id , SampleModel sample);

    }
}
