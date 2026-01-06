using Barber_Service.Models.DTOs;
using Barber_Service.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Barber_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public IActionResult Create(CustomerDTO dto)
        {
            var result = _customerService.Add(dto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _customerService.GetbyId(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CustomerDTO dto)
        {
            var result = _customerService.UpdateCustomer(dto, id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }


    }
}
