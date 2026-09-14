using Microsoft.AspNetCore.Mvc;
using MyFirstWebAPI.Models;
using MyFirstWebAPI.Services;
using Swashbuckle.AspNetCore.Annotations;  


namespace MyFirstWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly CustomerService _service;

    public CustomerController(CustomerService service)
    {
        _service = service;
    }

    /// <summary>
    /// Alle Kunden abrufen
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<Customer>>> GetAll()
    {
        try
        {
            var customers = await _service.GetAllCustomersAsync();
            return Ok(customers);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Kunde nach ID abrufen
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetById(int id)
    {
        try
        {
            var customer = await _service.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound($"Kunde mit ID {id} nicht gefunden");

            return Ok(customer);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Neuen Kunden erstellen
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Customer>> Create([FromBody] Customer customer)
    {
        try
        {
            var created = await _service.CreateCustomerAsync(customer);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Kunden aktualisieren
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] Customer customer)
    {
        try
        {
            var success = await _service.UpdateCustomerAsync(id, customer);
            if (!success)
                return NotFound($"Kunde mit ID {id} nicht gefunden");

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Kunden löschen
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var success = await _service.DeleteCustomerAsync(id);
            if (!success)
                return NotFound($"Kunde mit ID {id} nicht gefunden");

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
