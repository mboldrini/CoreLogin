using CoreLogin_Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CoreLogin_Domain.Repositories
{
  public interface IModuleRepository
  {
    Task<ActionResult<Module>> AddModuleAsync(Module module);
    Task<ActionResult<Module>> GetModuleByNameAsync(string name);
    Task<ActionResult<Module>> GetModuleByIdAsync(int id);
    Task<IEnumerable<Module>> GetModulesAsync();
    Task<ActionResult<Module>> UpdateModuleAsync(int id, Module module);
    Task<ActionResult<Module>> EnableOrDisableModuleAsync(int id, bool active);
  }
}
