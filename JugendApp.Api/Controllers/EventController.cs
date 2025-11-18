using AutoMapper;
using JugendApp.DTOs;
using JugendApp.SharedModels.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JugendApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly ApiDBContext _context;
        private readonly IMapper _mapper;

        public EventController(ApiDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SimpleEventDto>>> GetEvents()
        {
            var events = await _context.Events
                .Include(e => e.Location).ThenInclude(l => l.Address)
                .Include(e => e.CreatedBy)
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<SimpleEventDto>>(events));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SimpleEventDto>> GetEvent(int id)
        {
            var evt = await _context.Events
                .Include(e => e.Location).ThenInclude(l => l.Address)
                .Include(e => e.CreatedBy)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (evt is null) return NotFound();
            return Ok(_mapper.Map<SimpleEventDto>(evt));
        }

        [HttpPost]
        public async Task<ActionResult<SimpleEventDto>> CreateEvent(SimpleEventDto dto)
        {
            var evt = _mapper.Map<SimpleEvent>(dto);
            _context.Events.Add(evt);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<SimpleEventDto>(evt);
            return CreatedAtAction(nameof(GetEvent), new { id = evt.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, SimpleEventDto dto)
        {
            if (id != dto.Id) return BadRequest();

            var evt = await _context.Events
                .Include(e => e.Location).ThenInclude(l => l.Address)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (evt is null) return NotFound();

            _mapper.Map(dto, evt);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var evt = await _context.Events.FindAsync(id);
            if (evt is null) return NotFound();

            _context.Events.Remove(evt);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
