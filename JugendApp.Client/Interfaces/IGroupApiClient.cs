using JugendApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JugendApp.Client.Interfaces
{
    public interface IGroupApiClient
    {
        Task<IEnumerable<GroupDto>> GetAllAsync();
        Task<GroupDto?> GetByIdAsync(int id);
        Task<GroupDto> CreateAsync(GroupDto dto);
        Task UpdateAsync(GroupDto dto);
        Task DeleteAsync(int id);

        // Spezielle Methoden
        Task AddMemberAsync(int groupId, GroupMemberDto member);
        Task RemoveMemberAsync(int groupId, int memberId);
    }
}
