using JugendApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JugendApp.Client.Interfaces
{
    public interface IEventApiClient
    {
        Task<IEnumerable<SimpleEventDto>> GetAllAsync();
        Task<SimpleEventDto?> GetByIdAsync(int id);
        Task<SimpleEventDto> CreateAsync(SimpleEventDto dto);
        Task UpdateAsync(SimpleEventDto dto);
        Task DeleteAsync(int id);

        // Spezielle Methoden
        Task<IEnumerable<SimpleEventDto>> GetEventsByPersonAsync(int personId);
        Task<IEnumerable<SimpleEventDto>> GetEventsByLocationAsync(int locationId);
    }
}
