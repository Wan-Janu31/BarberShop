using AutoMapper;
using Barber_Service.Models;
using Barber_Service.Models.DTOs;
using Barber_Service.Repository.Interfaces;
using Barber_Service.Service.Interfaces;

namespace Barber_Service.Service.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // CREATE – dùng khi booking
        public CustomerDTO Add(CustomerDTO customerDto)
        {
            // check trùng phone (rất quan trọng)
            var exist = _repository.GetByPhone(customerDto.Phone);
            if (exist != null)
            {
                return _mapper.Map<CustomerDTO>(exist);
            }

            var customer = _mapper.Map<Customer>(customerDto);

            // KHÔNG set CreatedAt
            // KHÔNG set IsGuest
            // DB sẽ tự xử lý

            var created = _repository.Add(customer);
            return _mapper.Map<CustomerDTO>(created);
        }

        // GET BY ID
        public CustomerDTO? GetbyId(int id)
        {
            var customer = _repository.GetbyId(id);
            if (customer == null)
                return null;

            return _mapper.Map<CustomerDTO>(customer);
        }

        

        // UPDATE – chỉ cho sửa name, address, note
        public CustomerDTO? UpdateCustomer(CustomerDTO customerDto, int id)
        {
            var updated = _repository.UpdateCustomer(
                _mapper.Map<Customer>(customerDto), id);

            if (updated == null)
                return null;

            return _mapper.Map<CustomerDTO>(updated);
        }
    }
}
