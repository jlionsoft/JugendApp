using JugendApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JugendApp.Client.Interfaces
{
    public interface IInstrumentApiClient
    {
        Task<IEnumerable<InstrumentDto>> GetAllAsync();
        Task<InstrumentDto?> GetByIdAsync(int id);
        Task<InstrumentDto> CreateAsync(InstrumentDto dto);
        Task UpdateAsync(InstrumentDto dto);
        Task DeleteAsync(int id);

        // Spezielle Methoden
        Task<IEnumerable<InstrumentDto>> SearchByNameAsync(string name);
    }
}
