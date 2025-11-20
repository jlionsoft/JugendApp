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
    public class GroupApiClient : IGroupApiClient
    {
        private readonly HttpClient _http;
        public GroupApiClient(HttpClient http) => _http = http;

        public async Task<IEnumerable<GroupDto>> GetAllAsync() =>
            await _http.GetFromJsonAsync<IEnumerable<GroupDto>>("api/group") ?? [];

        public async Task<GroupDto?> GetByIdAsync(int id) =>
            await _http.GetFromJsonAsync<GroupDto>($"api/group/{id}");

        public async Task<GroupDto> CreateAsync(GroupDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/group", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<GroupDto>()!;
        }

        public async Task UpdateAsync(GroupDto dto) =>
            (await _http.PutAsJsonAsync($"api/group/{dto.Id}", dto)).EnsureSuccessStatusCode();

        public async Task DeleteAsync(int id) =>
            (await _http.DeleteAsync($"api/group/{id}")).EnsureSuccessStatusCode();

        public async Task AddMemberAsync(int groupId, GroupMemberDto member) =>
            (await _http.PostAsJsonAsync($"api/group/{groupId}/members", member)).EnsureSuccessStatusCode();

        public async Task RemoveMemberAsync(int groupId, int memberId) =>
            (await _http.DeleteAsync($"api/group/{groupId}/members/{memberId}")).EnsureSuccessStatusCode();
    }
}
