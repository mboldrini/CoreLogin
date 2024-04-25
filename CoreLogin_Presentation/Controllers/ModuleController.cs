using CoreLogin_Domain.Converters.DTO;
using CoreLogin_Domain.Entities;
using CoreLogin_Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreLogin_Presentation.Controllers
{
  [Route("v1/module")]
  public class ModuleController : ControllerBase
  {
    private readonly IModuleRepository _moduleRepository;

    public ModuleController(IModuleRepository moduleRepository)
    {
      _moduleRepository = moduleRepository;
    }

    [HttpPost]
    [Route("create")]
    [AllowAnonymous]
    public async Task<ActionResult<ModuleResultDTO>> AddModuleAsync([FromBody] ModuleRequestDTO module)
    {
      var newModule = await _moduleRepository.AddModuleAsync(module);
      if (newModule == null)
      {
        return BadRequest("Module already exists");
      }
      return Ok(module);

    }

    [HttpGet]
    [Route("{name}")]
    [AllowAnonymous]
    public async Task<ActionResult<ModuleResultDTO>> GetModuleByNameAsync([FromRoute] string name)
    {
      var module = await _moduleRepository.GetModuleByNameAsync(name);
     
      return module;
    }

    [HttpGet]
    [Route("id/{id:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetModuleByIdAsync([FromRoute] int id)
    {
      var module = await _moduleRepository.GetModuleByIdAsync(id);
      if (module == null)
      {
        return NotFound("Module not found");
      }
      return Ok(module);
    }

    [HttpGet]
    [Route("all")]
    [AllowAnonymous]
    public async Task<IActionResult> GetModulesAsync()
    {
      var modules = await _moduleRepository.GetModulesAsync();
      return Ok(modules);
    }

    [HttpPut]
    [Route("update/{id:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateModuleAsync([FromRoute] int id, [FromBody] Module module)
    {
      var updatedModule = await _moduleRepository.UpdateModuleAsync(id, module);
      if (updatedModule == null)
      {
        return NotFound("Module not found");
      }
      return Ok(updatedModule);
    }

    [HttpPut]
    [Route("active/{id:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> EnableOrDisableModuleAsync([FromRoute] int id, [FromBody] bool active)
    {
      var module = await _moduleRepository.EnableOrDisableModuleAsync(id, active);
      if (module == null)
      {
        return NotFound("Module not found");
      }
      return Ok(module);
    }

  }
}
