using Barber_Service.Models;
using Barber_Service.Models.DTOs;

namespace Barber_Service.Service.Interfaces
{
    public interface ICustomerService
    {
        CustomerDTO Add(CustomerDTO customer);
        CustomerDTO GetbyId(int id);
        CustomerDTO UpdateCustomer(CustomerDTO customer, int id);
       
    }
}
