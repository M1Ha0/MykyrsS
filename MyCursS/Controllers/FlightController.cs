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

    public class FlightController : ControllerBase
    {
        private readonly FlightService flightService;

        public FlightController(FlightService service)
        {
            this.flightService = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flight>>> GetAllFlights()
        {
            return Ok(await flightService.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Flight>> GetFlightById(int id)
        {
            var fligh = await flightService.GetById(id);
            if (fligh == null) return NotFound();
            return Ok(fligh);
        }
        [HttpPost]
        public async Task<ActionResult<Flight>> CreateFlight([FromBody] Flight fligh)
        {
            await flightService.Create(fligh);
            return CreatedAtAction(nameof(GetFlightById), new { Id = fligh.NumberFlight }, fligh);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Flight>> UpdateFlight(int id, [FromBody] Flight fligh)
        {
            if (fligh.NumberFlight != id) return BadRequest();
            await flightService.Update(fligh);
            return Ok(fligh);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await flightService.Delete(id);
            return NoContent();
        }
    }
}