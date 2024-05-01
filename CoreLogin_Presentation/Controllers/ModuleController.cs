using CoreLogin_Domain.Converters.DTO;
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

    /// <summary>
    /// Endpoint to create a new Module
    /// </summary>
    /// <param name="module">ModuleRequestDTP</param>
    /// <returns>ModuleResultDTO</returns>
    [HttpPost]
    [Route("create")]
    [AllowAnonymous]
    public async Task<ActionResult<ModuleResultDTO>> AddModuleAsync([FromBody] ModuleRequestDTO module)
    {
      var moduleFound = await _moduleRepository.AddModuleAsync(module);
      if(moduleFound == null)
      {
        return BadRequest("Error on create a new module");
      }

      return new OkObjectResult(moduleFound);
    }


    /// <summary>
    /// Enndpoint to get a module by name
    /// </summary>
    /// <param name="name">Name of the module</param>
    /// <returns>ModuleResultDTO</returns>
    [HttpGet]
    [Route("{name}")]
    [AllowAnonymous]
    public async Task<ActionResult<ModuleResultDTO>> GetModuleByNameAsync([FromRoute] string name)
    {
      var module = await _moduleRepository.GetModuleByNameAsync(name);
      if (module == null)
      {
        return NotFound("Module not found");
      }

      return new OkObjectResult(module);
    }

    /// <summary>
    /// Endpoint to get a module by id
    /// </summary>
    /// <param name="id">ID of the module</param>
    /// <returns>ModuleResultDTO</returns>
    [HttpGet]
    [Route("id/{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<ModuleResultDTO>> GetModuleByIdAsync([FromRoute] int id)
    {
      var module = await _moduleRepository.GetModuleByIdAsync(id);
      if (module == null)
      {
        return NotFound("Module not found");
      }

      return new OkObjectResult(module);
    }

    /// <summary>
    /// Endpoint to get all modules
    /// </summary>
    /// <returns>ModuleResultDTO</returns>
    [HttpGet]
    [Route("all")]
    [AllowAnonymous]
    public async Task<ActionResult<ModuleResultDTO>> GetModulesAsync()
    {
      var modules = await _moduleRepository.GetModulesAsync();
      return Ok(modules);
    }

    /// <summary>
    /// Endpoint to update a module
    /// </summary>
    /// <param name="id">ID of the module</param>
    /// <param name="module">Module Infos</param>
    /// <returns></returns>
    [HttpPut]
    [Route("update/{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<ModuleResultDTO>> UpdateModuleAsync([FromRoute] int id, [FromBody] ModuleRequestDTO module)
    {
      var updatedModule = await _moduleRepository.UpdateModuleAsync(id, module);
      if (updatedModule == null)
      {
        return new NotFoundObjectResult("Module not found");
      }
      return new OkObjectResult(updatedModule);
    }

    /// <summary>
    /// Endpoint to enable or disable a module
    /// </summary>
    /// <param name="id">ID of the module</param>
    /// <returns>ModuleResultDTO</returns>
    [HttpPut]
    [Route("active/{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<ModuleResultDTO>> EnableOrDisableModuleAsync([FromRoute] int id)
    {
      var module = await _moduleRepository.EnableOrDisableModuleAsync(id);
      if (module == null)
      {
        return new NotFoundObjectResult("Module not found");
      }

      return new OkObjectResult(module);
    }

  }
}
