using AutoMapper;
using JugendApp.DTOs;
using JugendApp.SharedModels.Person;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JugendApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ApiDBContext _context;
        private readonly IMapper _mapper;

        public PersonController(ApiDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetPersons()
        {
            var persons = await _context.Persons
                .Include(p => p.ContactOptions)
                .Include(p => p.Instruments).ThenInclude(s => s.Instrument)
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<PersonDto>>(persons));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDto>> GetPerson(int id)
        {
            var person = await _context.Persons
                .Include(p => p.ContactOptions)
                .Include(p => p.Instruments).ThenInclude(s => s.Instrument)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (person is null) return NotFound();
            return Ok(_mapper.Map<PersonDto>(person));
        }

        [HttpPost]
        public async Task<ActionResult<PersonDto>> CreatePerson(PersonDto dto)
        {
            var person = _mapper.Map<Person>(dto);
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<PersonDto>(person);
            return CreatedAtAction(nameof(GetPerson), new { id = person.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(int id, PersonDto dto)
        {
            if (id != dto.Id) return BadRequest();

            var person = await _context.Persons
                .Include(p => p.ContactOptions)
                .Include(p => p.Instruments)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (person is null) return NotFound();

            _mapper.Map(dto, person);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person is null) return NotFound();

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
