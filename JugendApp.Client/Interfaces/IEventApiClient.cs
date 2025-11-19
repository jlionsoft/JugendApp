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
        Task<IEnumerable<EventDto>> GetAllAsync();
        Task<EventDto?> GetByIdAsync(int id);
        Task<EventDto> CreateAsync(EventDto dto);
        Task UpdateAsync(EventDto dto);
        Task DeleteAsync(int id);

        // Spezielle Methoden
        Task<IEnumerable<EventDto>> GetEventsByPersonAsync(int personId);
        Task<IEnumerable<EventDto>> GetEventsByLocationAsync(int locationId);


        Task InvitePersonAsync(InvitationDto dto);
        Task RemoveInvitationAsync(int eventId, int personId);
        Task<IEnumerable<InvitationDto>> GetInvitationsAsync(int eventId);
    }
}
