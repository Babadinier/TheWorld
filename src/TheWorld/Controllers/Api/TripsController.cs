using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheWorld.Models;
using TheWorld.ViewModels;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TheWorld.Controllers.Api
{
    [Route("api/trips")]
    [Authorize]
    public class TripsController : Controller
    {
        private ILogger<TripsController> _logger;
        private IWorldRepository _repository;

        public TripsController(IWorldRepository repository, ILogger<TripsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var result = _repository.GetTripsByUsername(this.User.Identity.Name);
                return Ok(Mapper.Map<IEnumerable<TripViewModel>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get All trips : {ex}");
                return BadRequest("Error occured");
            }

        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]TripViewModel theTrip)
        {
            if(ModelState.IsValid)
            {
                var newTrip = Mapper.Map<Trip>(theTrip);

                newTrip.UserName = User.Identity.Name;

                _repository.AddTrip(newTrip);

                if(await _repository.SaveChangesAsync())
                {
                    return Created($"api/trips/{theTrip.Name}", Mapper.Map<TripViewModel>(newTrip));
                }
            }

            return BadRequest("Failed to save the trip");
            
        }
    }
}
