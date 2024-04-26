using CoreLogin_Domain.Converters;
using CoreLogin_Domain.Converters.DTO;
using CoreLogin_Domain.Entities;
using CoreLogin_Domain.Repositories;
using CoreLogin_Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreLogin_Infrastructure.Repository
{
  public class ModuleRepository : IModuleRepository
  {
    public readonly DataContext _dbContext;

    public ModuleRepository(DataContext dbContext)
    {
      _dbContext = dbContext;
    }

    /// <summary>
    /// Create a new module
    /// </summary>
    /// <param name="module">Module object</param>
    /// <returns>Module</returns>
    public async Task<ActionResult<ModuleResultDTO>> AddModuleAsync(ModuleRequestDTO module)
    {
      var moduleExists = await _dbContext.Modules.FirstOrDefaultAsync(m => m.Name == module.Name);
      if (moduleExists != null)
      {
        return new OkObjectResult(moduleExists);
      }

      var moduleRequestConverted = ModuleConverter.ModuleRequest(module);

      await _dbContext.Modules.AddAsync(moduleRequestConverted);
      await _dbContext.SaveChangesAsync();

      var moduleConverted = ModuleConverter.ModuleResult(moduleRequestConverted);

      return new OkObjectResult(moduleConverted);
    }

    /// <summary>
    /// Get Module by name
    /// </summary>
    /// <param name="name">Name of the module</param>
    /// <returns>Module</returns>
    public async Task<ActionResult<ModuleResultDTO>> GetModuleByNameAsync(string name)
    {
      // return module with all groups related
      var module = await _dbContext.Modules.Include(m => m.GroupModules).ThenInclude(gm => gm.Group).FirstOrDefaultAsync(m => m.Name == name);
      if (module == null)
      {
        return new NotFoundObjectResult("Module not found");
      }

      var moduleConverted = ModuleConverter.ModuleResult(module);
      return new OkObjectResult(moduleConverted);

    }

    /// <summary>
    ///  Get Module by Id
    /// </summary>
    /// <param name="id">ID of the module</param>
    /// <returns>Module</returns>
    public async Task<ActionResult<ModuleResultDTO>> GetModuleByIdAsync(int id)
    {
      // return module with all groups related
      var module = await _dbContext.Modules.Include(m => m.GroupModules).ThenInclude(gm => gm.Group).FirstOrDefaultAsync(m => m.Id == id);
      if (module == null)
      {
        return new NotFoundObjectResult("Module not found");
      }

      var moduleConverted = ModuleConverter.ModuleResult(module);
      return new OkObjectResult(moduleConverted);
    }

    /// <summary>
    /// Get all Modules
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<ModuleResultDTO>> GetModulesAsync()
    {
      // return all modules with all groups related
      var allModules = await _dbContext.Modules.Include(m => m.GroupModules).ThenInclude(gm => gm.Group).ToListAsync();

      var allModulesConverted = allModules.Select(m => ModuleConverter.ModuleResult(m)).ToList();

      return allModulesConverted;
    }

    /// <summary>
    /// Update Module
    /// </summary>
    /// <param name="id">Id</param>
    /// <param name="module">Module Object</param>
    /// <returns></returns>
    public async Task<ActionResult<ModuleResultDTO>> UpdateModuleAsync(int id, ModuleRequestDTO module)
    {
      var moduleExist = await _dbContext.Modules.FirstOrDefaultAsync(m => m.Id == id);
      if (module == null)
      {
        return new NotFoundResult();
      }

      moduleExist.Name = module.Name;
      moduleExist.Active = module.Active;
      moduleExist.Description = module.Description;

      _dbContext.Modules.Update(moduleExist);
      await _dbContext.SaveChangesAsync();

      var moduleConverted = ModuleConverter.ModuleResult(moduleExist);

      return new OkObjectResult(moduleConverted);
    }

    /// <summary>
    /// Enable or Disable Module
    /// </summary>
    /// <param name="id">ID</param>
    /// <param name="active">Active: True/False</param>
    /// <returns></returns>
    public async Task<ActionResult<ModuleResultDTO>> EnableOrDisableModuleAsync(int id)
    {
      var moduleExist = await _dbContext.Modules.Include(m => m.GroupModules).ThenInclude(gm => gm.Group).FirstOrDefaultAsync(m => m.Id == id);

      if (moduleExist == null)
      {
        return new NotFoundResult();
      }

      moduleExist.Active = !moduleExist.Active;

      _dbContext.Modules.Update(moduleExist);

      await _dbContext.SaveChangesAsync();

      var moduleConverted = ModuleConverter.ModuleResult(moduleExist);

      return new OkObjectResult(moduleConverted);

    }

  }
}
