using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WemaAPI.OTPService.Contracts.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WemaAPI.Data.Repository.IRepository;
using WemaAPI.Models;
using static WemaAPI.OTPService.Contracts.Request.OTPRequest;
using static WemaAPI.OTPService.Contracts.Response.OTPResponse;

namespace WemaAPI.OTPService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OTPController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public OTPController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get OTP by phoneNumber
        /// </summary>
        // GET: api/<OTPController>
        [HttpGet("{phoneNumber}")]
        public async Task<IActionResult> Get(string phoneNumber)
        {
            //Initialize response parameters
            var response = new SendOTPResponse
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

            if (string.IsNullOrEmpty(phoneNumber) || string.IsNullOrWhiteSpace(phoneNumber))
            {
                return BadRequest("Endpoint does not accept null or empty parameters");
            }

            OTP otp = await _unitOfWork.OTP.GetFirstOrDefaultAsync(u => u.PhoneNumber.Equals(phoneNumber));

            if(otp == null)
            {
                error.Status = "404";
                error.Message = "Not found";
                error.Description = "No otp for phone number given";

                return StatusCode(StatusCodes.Status404NotFound, error);
            }


            return Ok(otp.Token);
        }



        /// <summary>
        /// Send OTP
        /// </summary>
        // POST api/<OTPController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SendOTPRequest request)
        {
            if (ModelState.IsValid)
            {
                //Initialize response parameters
                var response = new SendOTPResponse
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

                var objFromDb = await _unitOfWork.OTP.GetFirstOrDefaultAsync(u => u.PhoneNumber.Equals(request.PhoneNumber));
                Random rand = new Random();

                if (objFromDb == null)
                {
                    // load user with OTP
                    OTP otp = new OTP();
                    otp.PhoneNumber = request.PhoneNumber;
                    otp.Token = rand.Next(1000, 9999).ToString();

                    await _unitOfWork.OTP.AddAsync(otp);
                }
                else
                {
                    objFromDb.Token = rand.Next(1000, 9999).ToString();
                }

                await _unitOfWork.SaveAsync();

                response.Message = "OTP sent successfully";

                return StatusCode(StatusCodes.Status201Created, response);
            }

            return StatusCode(StatusCodes.Status400BadRequest);
        }

    }
}
