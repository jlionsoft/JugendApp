using JugendApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JugendApp.Client.Interfaces
{
    public interface IPersonApiClient
    {
        Task<IEnumerable<PersonDto>> GetAllAsync();
        Task<PersonDto?> GetByIdAsync(int id);
        Task<PersonDto> CreateAsync(PersonDto dto);
        Task UpdateAsync(PersonDto dto);
        Task DeleteAsync(int id);

        // Spezielle Methoden
        Task AddContactOptionAsync(int personId, ContactOptionDto option);
        Task RemoveContactOptionAsync(int personId, int contactOptionId);

        Task AddInstrumentSkillAsync(int personId, InstrumentSkillDto skill);
        Task RemoveInstrumentSkillAsync(int personId, int instrumentSkillId);
    }
}
