using JugendApp.DTOs;
using JugendApp.SharedModels.Groups;
using JugendApp.SharedModels.Person;


namespace JugendApp.SharedModels.Interfaces;








public interface IAddressApiClient
{
    Task<IEnumerable<AddressDto>> GetAllAsync();
    Task<AddressDto?> GetByIdAsync(int id);
    Task<AddressDto> CreateAsync(AddressDto dto);
    Task UpdateAsync(AddressDto dto);
    Task DeleteAsync(int id);
}