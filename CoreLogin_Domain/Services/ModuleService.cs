using CoreLogin_Domain.Converters.DTO;
using CoreLogin_Domain.Entities;
using CoreLogin_Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoreLogin_Application.Services
{
  public class ModuleService
  {
    private readonly IModuleRepository _moduleRepository;

    public ModuleService(IModuleRepository moduleRepository)
    {
      _moduleRepository = moduleRepository;
    }

    public async Task<IEnumerable<Module>> GetModulesAsync()
    {
      return await _moduleRepository.GetModulesAsync();
    }

    public async Task<ActionResult<Module>> GetModuleByIdAsync(int id)
    {
      return await _moduleRepository.GetModuleByIdAsync(id);
    }

    public async Task AddModuleAsync(ModuleRequestDTO module)
    {
      await _moduleRepository.AddModuleAsync(module);
    }

    public async Task UpdateModuleAsync(int id, Module module)
    {
      await _moduleRepository.UpdateModuleAsync(id, module);
    }

  }
}
