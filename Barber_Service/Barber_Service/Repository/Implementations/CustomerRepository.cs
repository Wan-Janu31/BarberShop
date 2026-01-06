using Barber_Service.Models;
using Barber_Service.Repository.Interfaces;

namespace Barber_Service.Repository.Implementations
{
    public class CustomerRepository : ICustomerRepository
    {
        public readonly BarberBookingDbContext _db;
        public CustomerRepository(BarberBookingDbContext db)
        {
            _db = db;
        }

        public Customer Add(Customer customer)
        {
            customer.IsGuest = true;
            customer.CreatedAt = DateTime.Now;

            _db.Customers.Add(customer);
            _db.SaveChanges();
            return customer;
        }


        public Customer GetbyId(int id)
        {
            var cus = _db.Customers.FirstOrDefault(c=> c.Id == id);
            return cus;
        }

        public Customer UpdateCustomer(Customer customer, int id)
        {
            var exist = _db.Customers.Find(id);
            if (exist == null)
                return null;

            exist.FullName = customer.FullName;
            exist.Phone = customer.Phone;
          
            exist.Address = customer.Address;
            exist.Note = customer.Note;
            _db.SaveChanges();
            return exist;
        }

        public Customer? GetByPhone(string phone)
        {
            return _db.Customers.FirstOrDefault(c => c.Phone == phone);
        }

    }
}
