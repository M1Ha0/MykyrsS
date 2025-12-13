using Microsoft.AspNetCore.Mvc;
using MyCursS.Models;
using MyCursS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace MyCursS.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]

    public class PassengerController : ControllerBase
    {
        private readonly PassengerService passengerService;

        public PassengerController(PassengerService service)
        {
            this.passengerService = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Passenger>>> GetAllPassengers()
        {
            return Ok(await passengerService.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Passenger>> GetPassengerById(int id)
        {
            var passenger = await passengerService.GetById(id);
            if (passenger == null) return NotFound();
            return Ok(passenger);
        }
        [HttpPost]
        public async Task<ActionResult<Passenger>> CreatePassenger([FromBody] Passenger passenge)
        {
            await passengerService.Create(passenge);
            return CreatedAtAction(nameof(GetPassengerById), new { Id = passenge.IdPassenger }, passenge);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Passenger>> UpdatePassenger(int id, [FromBody] Passenger passenge)
        {
            if (passenge.IdPassenger != id) return BadRequest();
            await passengerService.Update(passenge);
            return Ok(passenge);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await passengerService.Delete(id);
            return NoContent();
        }
    }
}