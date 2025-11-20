using JugendApp.Client.Interfaces;
using JugendApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace JugendApp.Client.Implementations
{
    public class EventApiClient : IEventApiClient
    {
        private readonly HttpClient _http;
        public EventApiClient(HttpClient http) => _http = http;

        public async Task<IEnumerable<EventDto>> GetAllAsync() =>
            await _http.GetFromJsonAsync<IEnumerable<EventDto>>("api/event") ?? [];

        public async Task<EventDto?> GetByIdAsync(int id) =>
            await _http.GetFromJsonAsync<EventDto>($"api/event/{id}");

        public async Task<EventDto> CreateAsync(EventDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/event", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<EventDto>()!;
        }

        public async Task UpdateAsync(EventDto dto) =>
            (await _http.PutAsJsonAsync($"api/event/{dto.Id}", dto)).EnsureSuccessStatusCode();

        public async Task DeleteAsync(int id) =>
            (await _http.DeleteAsync($"api/event/{id}")).EnsureSuccessStatusCode();

        public async Task<IEnumerable<EventDto>> GetEventsByPersonAsync(int personId) =>
            await _http.GetFromJsonAsync<IEnumerable<EventDto>>($"api/event/person/{personId}") ?? [];

        public async Task<IEnumerable<EventDto>> GetEventsByLocationAsync(int locationId) =>
            await _http.GetFromJsonAsync<IEnumerable<EventDto>>($"api/event/location/{locationId}") ?? [];

        public async Task InvitePersonAsync(InvitationDto dto)
        {
            var response = await _http.PostAsJsonAsync($"api/event/{dto.EventId}/invitations", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task RemoveInvitationAsync(int eventId, int personId)
        {
            var response = await _http.DeleteAsync($"api/event/{eventId}/invitations/{personId}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<InvitationDto>> GetInvitationsAsync(int eventId) =>
            await _http.GetFromJsonAsync<IEnumerable<InvitationDto>>($"api/event/{eventId}/invitations") ?? [];
    }
}
