using Microsoft.AspNetCore.Mvc;
using MyFirstWebAPI.Models;
using MyFirstWebAPI.Services;   

namespace MyFirstWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeService _employeeService;

    public EmployeeController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }


    [HttpGet]
    public async Task<ActionResult<string>> GetEmployeeName()
    {

         var employeeName = await _employeeService.GetEmployeeName();
         return Ok(employeeName);
        
    }
}