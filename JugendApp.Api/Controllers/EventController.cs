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
        public async Task<ActionResult<IEnumerable<EventDto>>> GetEvents()
        {
            var events = await _context.Events
                .Include(e => e.Location).ThenInclude(l => l.Address)
                .Include(e => e.CreatedBy)
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<EventDto>>(events));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventDto>> GetEvent(int id)
        {
            var evt = await _context.Events
                .Include(e => e.Location).ThenInclude(l => l.Address)
                .Include(e => e.CreatedBy)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (evt is null) return NotFound();
            return Ok(_mapper.Map<EventDto>(evt));
        }

        [HttpPost]
        public async Task<ActionResult<EventDto>> CreateEvent(EventDto dto)
        {
            var evt = _mapper.Map<Event>(dto);
            _context.Events.Add(evt);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<EventDto>(evt);
            return CreatedAtAction(nameof(GetEvent), new { id = evt.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, EventDto dto)
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

        [HttpPost("{id}/invitations")]
        public async Task<IActionResult> InvitePerson(int id, InvitationDto dto)
        {
            if (id != dto.EventId) return BadRequest();

            var exists = await _context.Invitations.AnyAsync(i => i.EventId == id && i.PersonId == dto.PersonId);
            if (exists) return Conflict("Person already invited.");

            var entity = _mapper.Map<Invitation>(dto);
            _context.Invitations.Add(entity);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<InvitationDto>(entity));
        }

        [HttpDelete("{id}/invitations/{personId}")]
        public async Task<IActionResult> RemoveInvitation(int id, int personId)
        {
            var entity = await _context.Invitations.FirstOrDefaultAsync(i => i.EventId == id && i.PersonId == personId);
            if (entity == null) return NotFound();

            _context.Invitations.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{id}/invitations")]
        public async Task<ActionResult<IEnumerable<InvitationDto>>> GetInvitations(int id)
        {
            var invitations = await _context.Invitations.Where(i => i.EventId == id).ToListAsync();
            return _mapper.Map<List<InvitationDto>>(invitations);
        }
    }
}
