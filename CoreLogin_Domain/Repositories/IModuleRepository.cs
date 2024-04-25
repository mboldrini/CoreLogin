using CoreLogin_Domain.Converters.DTO;
using CoreLogin_Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CoreLogin_Domain.Repositories
{
  public interface IModuleRepository
  {
    Task<ActionResult<ModuleResultDTO>> AddModuleAsync(ModuleRequestDTO module);
    Task<ActionResult<ModuleResultDTO>> GetModuleByNameAsync(string name);
    Task<ActionResult<Module>> GetModuleByIdAsync(int id);
    Task<IEnumerable<Module>> GetModulesAsync();
    Task<ActionResult<Module>> UpdateModuleAsync(int id, Module module);
    Task<ActionResult<Module>> EnableOrDisableModuleAsync(int id, bool active);
  }
}
