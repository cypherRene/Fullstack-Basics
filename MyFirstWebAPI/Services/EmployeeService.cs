using MyFirstWebAPI.Models;
using MyFirstWebAPI.Repository;

namespace MyFirstWebAPI.Services;

public class EmployeeService
{
    string employeeName = "Max Mustermann"; // Example employee name
    public async Task<string> GetEmployeeName()
    {
        // Implementation for getting employee name by ID
        return employeeName;
    }
}