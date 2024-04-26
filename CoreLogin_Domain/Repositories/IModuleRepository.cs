using CoreLogin_Domain.Converters.DTO;
using CoreLogin_Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CoreLogin_Domain.Repositories
{
  public interface IModuleRepository
  {
    Task<ActionResult<ModuleResultDTO>> AddModuleAsync(ModuleRequestDTO module);
    Task<ActionResult<ModuleResultDTO>> GetModuleByNameAsync(string name);
    Task<ActionResult<ModuleResultDTO>> GetModuleByIdAsync(int id);
    Task<IEnumerable<ModuleResultDTO>> GetModulesAsync();
    Task<ActionResult<ModuleResultDTO>> UpdateModuleAsync(int id, ModuleRequestDTO module);
    Task<ActionResult<ModuleResultDTO>> EnableOrDisableModuleAsync(int id);
  }
}
