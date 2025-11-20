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
    public class LocationApiClient : ILocationApiClient
    {
        private readonly HttpClient _http;
        public LocationApiClient(HttpClient http) => _http = http;

        public async Task<IEnumerable<LocationDto>> GetAllAsync() =>
            await _http.GetFromJsonAsync<IEnumerable<LocationDto>>("api/location") ?? [];

        public async Task<LocationDto?> GetByIdAsync(int id) =>
            await _http.GetFromJsonAsync<LocationDto>($"api/location/{id}");

        public async Task<LocationDto> CreateAsync(LocationDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/location", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<LocationDto>()!;
        }

        public async Task UpdateAsync(LocationDto dto) =>
            (await _http.PutAsJsonAsync($"api/location/{dto.Id}", dto)).EnsureSuccessStatusCode();

        public async Task DeleteAsync(int id) =>
            (await _http.DeleteAsync($"api/location/{id}")).EnsureSuccessStatusCode();

        public async Task<IEnumerable<LocationDto>> SearchByCityAsync(string city) =>
            await _http.GetFromJsonAsync<IEnumerable<LocationDto>>($"api/location/search/city/{city}") ?? [];

        public async Task<IEnumerable<LocationDto>> SearchByPostalCodeAsync(string postalCode) =>
            await _http.GetFromJsonAsync<IEnumerable<LocationDto>>($"api/location/search/postal/{postalCode}") ?? [];
    }
}
