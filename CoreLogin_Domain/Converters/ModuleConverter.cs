﻿using CoreLogin_Domain.Converters.DTO;
using CoreLogin_Domain.Entities;
using CoreLogin_Domain.Entities.Relations;

namespace CoreLogin_Domain.Converters
{
  public class ModuleConverter
  {
    /// <summary>
    /// Convert Module to ModuleDTO
    /// </summary>
    /// <param name="module">Module</param>
    /// <returns>ModuleResultDTO</returns>
    public static ModuleResultDTO ModuleResult(Module module)
    {
      return new ModuleResultDTO
      {
        Id = module.Id,
        Name = module.Name,
        Description = module.Description,
        Active = module.Active,
        Groups = module.GroupModules?.Select(gm => gm.Group.Name) ?? Enumerable.Empty<string>(),
        Created_at = module.Created_at,
        Updated_at = module.Updated_at
      };
    }

    /// <summary>
    /// Convert ModuleRequestDTO to Module
    /// </summary>
    /// <param name="moduleRequest">ModuleRequestDTO</param>
    /// <returns>Module</returns>
    public static Module ModuleRequest(ModuleRequestDTO moduleRequest)
    {
      return new Module
      {
        Name = moduleRequest.Name,
        Description = moduleRequest.Description,
        Active = moduleRequest.Active
      };
    }


  }
}
