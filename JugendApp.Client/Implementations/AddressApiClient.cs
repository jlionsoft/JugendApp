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
    public class AddressApiClient : IAddressApiClient
    {
        private readonly HttpClient _http;
        public AddressApiClient(HttpClient http) => _http = http;

        public async Task<IEnumerable<AddressDto>> GetAllAsync() =>
            await _http.GetFromJsonAsync<IEnumerable<AddressDto>>("api/address") ?? [];

        public async Task<AddressDto?> GetByIdAsync(int id) =>
            await _http.GetFromJsonAsync<AddressDto>($"api/address/{id}");

        public async Task<AddressDto> CreateAsync(AddressDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/address", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<AddressDto>()!;
        }

        public async Task UpdateAsync(AddressDto dto) =>
            (await _http.PutAsJsonAsync($"api/address/{dto.Id}", dto)).EnsureSuccessStatusCode();

        public async Task DeleteAsync(int id) =>
            (await _http.DeleteAsync($"api/address/{id}")).EnsureSuccessStatusCode();

        public async Task<IEnumerable<AddressDto>> SearchByCityAsync(string city) =>
            await _http.GetFromJsonAsync<IEnumerable<AddressDto>>($"api/address/search/city/{city}") ?? [];

        public async Task<IEnumerable<AddressDto>> SearchByPostalCodeAsync(string postalCode) =>
            await _http.GetFromJsonAsync<IEnumerable<AddressDto>>($"api/address/search/postal/{postalCode}") ?? [];
    }
}