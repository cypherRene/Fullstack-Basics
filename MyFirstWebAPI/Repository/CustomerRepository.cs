using MyFirstWebAPI.Models;
using System.Text.Json;

namespace MyFirstWebAPI.Repository;

public class CustomerRepository
{
    private readonly string _dataPath = Path.Combine(AppContext.BaseDirectory, "Data", "customers.json");

    public async Task<List<Customer>> GetAllAsync()
    {
        try
        {
            if (!File.Exists(_dataPath))
                return new List<Customer>();

            var json = await File.ReadAllTextAsync(_dataPath);
            return JsonSerializer.Deserialize<List<Customer>>(json) ?? new List<Customer>();
        }
        catch
        {
            return new List<Customer>();
        }
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        var customers = await GetAllAsync();
        return customers.FirstOrDefault(c => c.Id == id);
    }

    public async Task<Customer> AddAsync(Customer customer)
    {
        var customers = await GetAllAsync();
        customer.Id = customers.Any() ? customers.Max(c => c.Id) + 1 : 1;
        customer.CreatedAt = DateTime.Now;
        customers.Add(customer);
        await SaveAsync(customers);
        return customer;
    }

    public async Task<bool> UpdateAsync(Customer customer)
    {
        var customers = await GetAllAsync();
        var existing = customers.FirstOrDefault(c => c.Id == customer.Id);
        if (existing == null) return false;

        existing.Name = customer.Name;
        existing.Email = customer.Email;
        existing.Phone = customer.Phone;
        await SaveAsync(customers);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var customers = await GetAllAsync();
        var customer = customers.FirstOrDefault(c => c.Id == id);
        if (customer == null) return false;

        customers.Remove(customer);
        await SaveAsync(customers);
        return true;
    }

    private async Task SaveAsync(List<Customer> customers)
    {
        var json = JsonSerializer.Serialize(customers, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_dataPath, json);
    }
}
