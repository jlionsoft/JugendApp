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
    public class PersonApiClient : IPersonApiClient
    {
        private readonly HttpClient _http;
        public PersonApiClient(HttpClient http) => _http = http;

        public async Task<IEnumerable<PersonDto>> GetAllAsync() =>
            await _http.GetFromJsonAsync<IEnumerable<PersonDto>>("api/person") ?? [];

        public async Task<PersonDto?> GetByIdAsync(int id) =>
            await _http.GetFromJsonAsync<PersonDto>($"api/person/{id}");

        public async Task<PersonDto> CreateAsync(PersonDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/person", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PersonDto>()!;
        }

        public async Task UpdateAsync(PersonDto dto) =>
            (await _http.PutAsJsonAsync($"api/person/{dto.Id}", dto)).EnsureSuccessStatusCode();

        public async Task DeleteAsync(int id) =>
            (await _http.DeleteAsync($"api/person/{id}")).EnsureSuccessStatusCode();

        public async Task AddContactOptionAsync(int personId, ContactOptionDto option) =>
            (await _http.PostAsJsonAsync($"api/person/{personId}/contactoptions", option)).EnsureSuccessStatusCode();

        public async Task RemoveContactOptionAsync(int personId, int contactOptionId) =>
            (await _http.DeleteAsync($"api/person/{personId}/contactoptions/{contactOptionId}")).EnsureSuccessStatusCode();

        public async Task AddInstrumentSkillAsync(int personId, InstrumentSkillDto skill) =>
            (await _http.PostAsJsonAsync($"api/person/{personId}/instrumentskills", skill)).EnsureSuccessStatusCode();

        public async Task RemoveInstrumentSkillAsync(int personId, int instrumentSkillId) =>
            (await _http.DeleteAsync($"api/person/{personId}/instrumentskills/{instrumentSkillId}")).EnsureSuccessStatusCode();
    }
}
