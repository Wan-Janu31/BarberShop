using Barber_Service.Models;

namespace Barber_Service.Repository.Interfaces
{
    public interface ICustomerRepository
    {
        Customer Add(Customer customer);
        Customer GetbyId(int id);
        Customer UpdateCustomer (Customer customer, int id);
        Customer? GetByPhone(string phone);

    }
}
