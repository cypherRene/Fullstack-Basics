using MyFirstWebAPI.Models;
using MyFirstWebAPI.Repository;

namespace MyFirstWebAPI.Services;

public class CustomerService
{
    private readonly CustomerRepository _repository;

    public CustomerService(CustomerRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Alle Kunden mit Validierung abrufen
    /// </summary>
    public async Task<List<Customer>> GetAllCustomersAsync()
    {
        return await _repository.GetAllAsync();
    }

    /// <summary>
    /// Kunden nach ID abrufen
    /// </summary>
    public async Task<Customer?> GetCustomerByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("ID muss größer als 0 sein");

        return await _repository.GetByIdAsync(id);
    }

    /// <summary>
    /// Neuen Kunden mit Validierung erstellen
    /// </summary>
    public async Task<Customer> CreateCustomerAsync(Customer customer)
    {
        if (string.IsNullOrWhiteSpace(customer.Name))
            throw new ArgumentException("Kundenname ist erforderlich");

        if (string.IsNullOrWhiteSpace(customer.Email))
            throw new ArgumentException("E-Mail ist erforderlich");

        // Email-Validierung
        if (!customer.Email.Contains("@"))
            throw new ArgumentException("Ungültiges E-Mail-Format");

        return await _repository.AddAsync(customer);
    }

    /// <summary>
    /// Kunden mit Validierung aktualisieren
    /// </summary>
    public async Task<bool> UpdateCustomerAsync(int id, Customer customer)
    {
        if (id <= 0)
            throw new ArgumentException("ID muss größer als 0 sein");

        if (string.IsNullOrWhiteSpace(customer.Name))
            throw new ArgumentException("Kundenname ist erforderlich");

        if (string.IsNullOrWhiteSpace(customer.Email))
            throw new ArgumentException("E-Mail ist erforderlich");

        customer.Id = id;
        return await _repository.UpdateAsync(customer);
    }

    /// <summary>
    /// Kunden mit Validierung löschen
    /// </summary>
    public async Task<bool> DeleteCustomerAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("ID muss größer als 0 sein");

        return await _repository.DeleteAsync(id);
    }
}
