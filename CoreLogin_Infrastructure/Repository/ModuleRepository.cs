using CoreLogin_Domain.Converters;
using CoreLogin_Domain.Converters.DTO;
using CoreLogin_Domain.Entities.Relations;
using CoreLogin_Domain.Repositories;
using CoreLogin_Infrastructure.Data;
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
    /// <param name="module">Module Request DTO</param>
    /// <returns>ModuleResultDTO</returns>
    public async Task<ModuleResultDTO> AddModuleAsync(ModuleRequestDTO module)
    {
      var moduleExists = await _dbContext.Modules.FirstOrDefaultAsync(m => m.Name == module.Name);
      if (moduleExists != null)
      {
        return ModuleConverter.ModuleResult(moduleExists);
      }

      var moduleRequestConverted = ModuleConverter.ModuleRequest(module);

      await _dbContext.Modules.AddAsync(moduleRequestConverted);

      // caso tenha grupos, encontra os grupos e adiciona na tabela de relacionamento
      if (module.Groups != null)
      {
        foreach (var group in module.Groups)
        {
          var groupExist = await _dbContext.Groups.FirstOrDefaultAsync(g => g.Name == group);
          if (groupExist != null)
          {
            var groupModule = new GroupModule
            {
              Group = groupExist,
              Module = moduleRequestConverted
            };

            await _dbContext.GroupModules.AddAsync(groupModule);
          }
        }
      }

      await _dbContext.SaveChangesAsync();

      return ModuleConverter.ModuleResult(moduleRequestConverted);
    }

    /// <summary>
    /// Get Module by name
    /// </summary>
    /// <param name="name">Name of the module</param>
    /// <returns>Module</returns>
    public async Task<ModuleResultDTO> GetModuleByNameAsync(string name)
    {
      // return module with all groups related
      var module = await _dbContext.Modules.Include(m => m.GroupModules).ThenInclude(gm => gm.Group).FirstOrDefaultAsync(m => m.Name == name);
      if (module == null)
      {
        return null;
      }

      return ModuleConverter.ModuleResult(module);
    }

    /// <summary>
    ///  Get Module by Id
    /// </summary>
    /// <param name="id">ID of the module</param>
    /// <returns>Module</returns>
    public async Task<ModuleResultDTO> GetModuleByIdAsync(int id)
    {
      // return module with all groups related
      var module = await _dbContext.Modules.Include(m => m.GroupModules).ThenInclude(gm => gm.Group).FirstOrDefaultAsync(m => m.Id == id);
      if (module == null)
      {
        return null;
      }

      return ModuleConverter.ModuleResult(module);
    }

    /// <summary>
    /// Get all Modules
    /// </summary>
    /// <returns>IEnumerable - ModuleResultDTO</returns>
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
    /// <returns>ModuleResultDTO</returns>
    public async Task<ModuleResultDTO> UpdateModuleAsync(int id, ModuleRequestDTO module)
    {
      var moduleExist = await _dbContext.Modules.Include(m => m.GroupModules).ThenInclude(gm => gm.Group).FirstOrDefaultAsync(m => m.Id == id);
      if (module == null)
      {
        return null;
      }

      moduleExist.Name = module.Name;
      moduleExist.Active = module.Active;
      moduleExist.Description = module.Description;
      moduleExist.Updated_at = DateTime.Now;

      if (module.Groups != null)
      {
        var groupModules = await _dbContext.GroupModules.Where(gm => gm.ModuleId == moduleExist.Id).ToListAsync();
        _dbContext.GroupModules.RemoveRange(groupModules);

        foreach (var group in module.Groups)
        {
          var groupExist = await _dbContext.Groups.FirstOrDefaultAsync(g => g.Name == group);
          if (groupExist != null)
          {
            var groupModule = new GroupModule
            {
              Group = groupExist,
              Module = moduleExist
            };

            await _dbContext.GroupModules.AddAsync(groupModule);
          }
        }
      }

      await _dbContext.SaveChangesAsync();

      return ModuleConverter.ModuleResult(moduleExist);
    }

    /// <summary>
    /// Enable or Disable Module
    /// </summary>
    /// <param name="id">ID</param>
    /// <param name="active">Active: True/False</param>
    /// <returns>ModuleResultDTO</returns>
    public async Task<ModuleResultDTO> EnableOrDisableModuleAsync(int id)
    {
      var moduleExist = await _dbContext.Modules.Include(m => m.GroupModules).ThenInclude(gm => gm.Group).FirstOrDefaultAsync(m => m.Id == id);

      if (moduleExist == null)
      {
        return null;
      }

      moduleExist.Active = !moduleExist.Active;
      moduleExist.Updated_at = DateTime.Now;

      _dbContext.Modules.Update(moduleExist);

      await _dbContext.SaveChangesAsync();

      return ModuleConverter.ModuleResult(moduleExist);
    }

  }
}
