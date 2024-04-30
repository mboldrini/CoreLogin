using CoreLogin.Domain.Entities.Enum;
using CoreLogin_Domain.Converters;
using CoreLogin_Domain.Converters.DTO;
using CoreLogin_Domain.Entities;
using CoreLogin_Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
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
      // validate if the permission.type is valid
      if (!Enum.IsDefined(typeof(EPermissionType), permission.Type))
      {
        return new BadRequestObjectResult("Invalid Permission Type");
      }

      var newPermission = await _permissionRepository.CreatePermissionAsync(permission);

      if(newPermission == null)
      {
        return new BadRequestObjectResult("Error on create new permission");
      }

      return new OkObjectResult(newPermission);
    }

    [HttpGet]
    [Route("all")]
    [AllowAnonymous]
    public async Task<IEnumerable<PermissionResultDTO>> GetPermissionAsync()
    {
      var permission = await _permissionRepository.GetPermissionsAsync();

      return permission.Select(p => PermissionConverter.PermissionResult(p));
    }

    [HttpDelete]
    [Route("delete")]
    [AllowAnonymous]
    public async Task<ActionResult> DeletePermissionAsync([FromBody] PermissionRequestDTO permission)
    {

      if (!Enum.IsDefined(typeof(EPermissionOperation), permission.Operation) || !Enum.IsDefined(typeof(EPermissionType), permission.Type))
      {
        return new BadRequestObjectResult("Invalid 'Permission Operation' or 'Type'");
      }

      var permissionDeleted = await _permissionRepository.DeletePermissionAsync(permission);

      if (permissionDeleted == null)
      {
        return new BadRequestObjectResult("Permission not found");
      }

      return new OkResult();
    }

  }
}
