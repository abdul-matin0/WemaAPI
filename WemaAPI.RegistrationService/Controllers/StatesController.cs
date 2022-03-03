using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WemaAPI.Data.Repository.IRepository;
using WemaAPI.Models;

namespace WemaAPI.RegistrationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public StatesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/<StatesController>
        [HttpGet]
        public async Task<IActionResult> GetStates()
        {
            IEnumerable<StateOfResidence> states = await _unitOfWork.StateOfResidence.GetAllAsync();

            return Ok(states);
        }


        /// <summary>
        /// Get all LGA by state
        /// </summary>
        // Get: api/<StatesController>/{state}
        [HttpGet("{state}")]
        public async Task<IActionResult> GetLGAByState(string state)
        {
            // validate state given
            StateOfResidence stateFromDb = await _unitOfWork.StateOfResidence.GetFirstOrDefaultAsync(u => !string.IsNullOrEmpty(u.State) && u.State.Trim().ToUpper().Equals(state.Trim().ToUpper()));

            // state object not found
            if (stateFromDb == null)
            {
                return NotFound();
            }

            // get all LGA by state
            IEnumerable<LGA> localGovernmentAreas = await _unitOfWork.LGA.GetAllAsync();
            IEnumerable<LGA> result = localGovernmentAreas.Where(u => u.StateOfResidenceId == stateFromDb.Id);

            return Ok(result);
        }
    }
}
