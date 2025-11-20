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
    public class InstrumentApiClient : IInstrumentApiClient
    {
        private readonly HttpClient _http;
        public InstrumentApiClient(HttpClient http) => _http = http;

        public async Task<IEnumerable<InstrumentDto>> GetAllAsync() =>
            await _http.GetFromJsonAsync<IEnumerable<InstrumentDto>>("api/instrument") ?? [];

        public async Task<InstrumentDto?> GetByIdAsync(int id) =>
            await _http.GetFromJsonAsync<InstrumentDto>($"api/instrument/{id}");

        public async Task<InstrumentDto> CreateAsync(InstrumentDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/instrument", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<InstrumentDto>()!;
        }

        public async Task UpdateAsync(InstrumentDto dto) =>
            (await _http.PutAsJsonAsync($"api/instrument/{dto.Id}", dto)).EnsureSuccessStatusCode();

        public async Task DeleteAsync(int id) =>
            (await _http.DeleteAsync($"api/instrument/{id}")).EnsureSuccessStatusCode();

        public async Task<IEnumerable<InstrumentDto>> SearchByNameAsync(string name) =>
            await _http.GetFromJsonAsync<IEnumerable<InstrumentDto>>($"api/instrument/search/{name}") ?? [];
    }
}
