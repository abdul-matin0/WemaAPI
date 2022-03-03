using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WemaAPI.Data.Repository.IRepository;
using WemaAPI.Models;
using WemaAPI.RegistrationService.Helpers;
using static WemaAPI.RegistrationService.Contracts.Requests.OnboardCustomerRequest;
using static WemaAPI.RegistrationService.Contracts.Responses.OnboardCustomerResponse;

namespace WemaAPI.RegistrationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnboardingController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public OnboardingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// User Onboarding Endpoint
        /// </summary>
        // POST api/<OnboardingController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserRequest request)
        {
            if (ModelState.IsValid)
            {
                //Initialize response parameters
                var response = new CreateUserResponse
                {
                    Message = null
                };

                //Initialize error parameters
                var error = new ErrorMsg
                {
                    Description = null,
                    Message = null,
                    Status = null
                };

                //Validate email address
                try
                {
                    var addr = new System.Net.Mail.MailAddress(request.Email);
                }
                catch
                {
                    error.Message = "Invalid Email Address";
                    error.Description = "Unable to create new user";
                    error.Status = "400";
                    return BadRequest(error);
                }


                // validate otp
                OTP userOTP = await _unitOfWork.OTP.GetFirstOrDefaultAsync(u => u.PhoneNumber.ToUpper().Equals(request.PhoneNumber));

                if(userOTP == null)
                {
                    error.Message = "No OTP reequested for user";
                    error.Description = "Generate OTP for user phone number";
                    error.Status = "400";
                    return BadRequest(error);
                }

                if (!userOTP.Token.Equals(request.OTP))
                {
                    error.Message = "Invalid OTP";
                    error.Description = "OTP is invalid";
                    error.Status = "400";
                    return BadRequest(error);
                }


                // validate LGA and State
                StateOfResidence stateFromDb = await _unitOfWork.StateOfResidence.GetFirstOrDefaultAsync(u => !string.IsNullOrEmpty(u.State) && u.State.Trim().ToUpper().Equals(request.StateOfResidence.Trim().ToUpper()));

                if(stateFromDb == null)
                {
                    error.Message = "Invalid State";
                    error.Description = "Unable to find given state";
                    error.Status = "400";
                    return BadRequest(error);
                }

                // validate LGA given is available under state given
                LGA localGovernmentFromDb = await _unitOfWork.LGA.GetFirstOrDefaultAsync(u => !string.IsNullOrEmpty(u.LocalGovernmentArea)
                && u.LocalGovernmentArea.Trim().ToUpper().Equals(request.LGA.Trim().ToUpper())
                && u.StateOfResidenceId.Equals(stateFromDb.Id));

                if(localGovernmentFromDb == null)
                {
                    // local government given is not available under the state given
                    error.Message = "Invalid LGA for State";
                    error.Description = "LGA does not map to state given";
                    error.Status = "400";
                    return BadRequest(error);
                }


                Customer customer = new Customer();

                customer.Email = request.Email;
                customer.PhoneNumber = request.PhoneNumber;
                //customer.StateOfResidenceId = localGovernmentFromDb.StateOfResidenceId;
                customer.LGAId = localGovernmentFromDb.Id;
                

                //  encrypt password
                customer.PasswordHash = EncryptPassword.EncryptHash(request.Password);

                await _unitOfWork.Customer.AddAsync(customer);
                await _unitOfWork.SaveAsync();

                //if no error return success Message

                response.Message = "Customer Created Successfully";

                return StatusCode(StatusCodes.Status201Created, response);
            }

            return StatusCode(StatusCodes.Status400BadRequest);
        }


        
    }
}
