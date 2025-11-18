using JugendApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JugendApp.Client.Interfaces
{
    public interface ILocationApiClient
    {
        Task<IEnumerable<LocationDto>> GetAllAsync();
        Task<LocationDto?> GetByIdAsync(int id);
        Task<LocationDto> CreateAsync(LocationDto dto);
        Task UpdateAsync(LocationDto dto);
        Task DeleteAsync(int id);

        // Spezielle Methoden
        Task<IEnumerable<LocationDto>> SearchByCityAsync(string city);
        Task<IEnumerable<LocationDto>> SearchByPostalCodeAsync(string postalCode);
    }
}
