using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WemaAPI.CustomerProfileService.Contracts.Responses;
using WemaAPI.Data.Repository.IRepository;
using WemaAPI.Models;

namespace WemaAPI.CustomerProfileService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;

        public CustomersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/<CustomersController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Customer> allCustomers = await _unitOfWork.Customer.GetAllAsync(includeProperties: "LGA");
            IEnumerable<LGA> allLGA = await _unitOfWork.LGA.GetAllAsync(includeProperties: "StateOfResidence");

            CustomerResponse response = new CustomerResponse();

            foreach(var customer in allCustomers)
            {
                var stateOfResidence = allLGA.FirstOrDefault(u => u.LocalGovernmentArea == customer.LGA.LocalGovernmentArea);

                response.Email = customer.Email;
                response.Id = customer.Id;
                response.PhoneNumber = customer.PhoneNumber;

                response.StateOfResidence = stateOfResidence.StateOfResidence.State;
                response.LGA = customer.LGA.LocalGovernmentArea;
            }

            return Ok(response);
        }

    }
}
