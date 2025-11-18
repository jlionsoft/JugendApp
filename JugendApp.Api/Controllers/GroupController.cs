using AutoMapper;
using JugendApp.DTOs;
using JugendApp.SharedModels.Groups;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JugendApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly ApiDBContext _context;
        private readonly IMapper _mapper;

        public GroupController(ApiDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupDto>>> GetGroups()
        {
            var groups = await _context.Groups
                .Include(g => g.Members).ThenInclude(m => m.Person)
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<GroupDto>>(groups));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GroupDto>> GetGroup(int id)
        {
            var group = await _context.Groups
                .Include(g => g.Members).ThenInclude(m => m.Person)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (group is null) return NotFound();
            return Ok(_mapper.Map<GroupDto>(group));
        }

        [HttpPost]
        public async Task<ActionResult<GroupDto>> CreateGroup(GroupDto dto)
        {
            var group = _mapper.Map<Group>(dto);
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<GroupDto>(group);
            return CreatedAtAction(nameof(GetGroup), new { id = group.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroup(int id, GroupDto dto)
        {
            if (id != dto.Id) return BadRequest();

            var group = await _context.Groups
                .Include(g => g.Members)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (group is null) return NotFound();

            _mapper.Map(dto, group);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group is null) return NotFound();

            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}