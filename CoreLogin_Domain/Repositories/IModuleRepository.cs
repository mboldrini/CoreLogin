using CoreLogin_Domain.Converters.DTO;

namespace CoreLogin_Domain.Repositories
{
  public interface IModuleRepository
  {
    Task<ModuleResultDTO> AddModuleAsync(ModuleRequestDTO module);
    Task<ModuleResultDTO> GetModuleByNameAsync(string name);
    Task<ModuleResultDTO> GetModuleByIdAsync(int id);
    Task<IEnumerable<ModuleResultDTO>> GetModulesAsync();
    Task<ModuleResultDTO> UpdateModuleAsync(int id, ModuleRequestDTO module);
    Task<ModuleResultDTO> EnableOrDisableModuleAsync(int id);
  }
}
