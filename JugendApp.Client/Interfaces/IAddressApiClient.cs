using JugendApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JugendApp.Client.Interfaces
{
    public interface IAddressApiClient
    {
        Task<IEnumerable<AddressDto>> GetAllAsync();
        Task<AddressDto?> GetByIdAsync(int id);
        Task<AddressDto> CreateAsync(AddressDto dto);
        Task UpdateAsync(AddressDto dto);
        Task DeleteAsync(int id);
    }
    
}
