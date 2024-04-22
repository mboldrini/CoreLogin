using CoreLogin_Domain.Converters;
using CoreLogin_Domain.Converters.DTO;
using CoreLogin_Domain.Entities;
using CoreLogin_Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace CoreLogin_Presentation.Controllers
{
  [Route("v1/permission")]
  public class PermissionController : ControllerBase // Inherit from ControllerBase
  {
    private readonly IPermissionRepository _permissionRepository;

    public PermissionController(IPermissionRepository permissionRepository)
    {
      _permissionRepository = permissionRepository;
    }

    [HttpPost]
    [Route("create")]
    [AllowAnonymous]
    public async Task<ActionResult<PermissionResultDTO>> AddPermissionAsync([FromBody] PermissionRequestDTO permission)
    {
      var newPermission = await _permissionRepository.CreatePermissionAsync(permission);

      return newPermission;
    }

    [HttpGet]
    [Route("all")]
    [AllowAnonymous]
    public async Task<IEnumerable<PermissionResultDTO>> GetPermissionAsync()
    {
      var permission = await _permissionRepository.GetPermissionsAsync();

      return permission.Select(p => PermissionConverter.PermissionResult(p));
    }

    [HttpGet]
    [Route("{operation}")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Get Permission by Operation Name")]
    public async Task<ActionResult<PermissionResultDTO>> GetPermissionByOperationAsync([FromRoute] string operation)
    {
      var permission = await _permissionRepository.GetPermissionByOperation(operation);

      return permission;
    }

  }
}
