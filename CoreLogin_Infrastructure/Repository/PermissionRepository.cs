using CoreLogin.Domain.Entities.Enum;
using CoreLogin_Domain.Converters;
using CoreLogin_Domain.Converters.DTO;
using CoreLogin_Domain.Entities;
using CoreLogin_Domain.Repositories;
using CoreLogin_Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreLogin_Infrastructure.Repository
{
  public class PermissionRepository : IPermissionRepository
  {

    public readonly DataContext _dbContext;

    public PermissionRepository(DataContext dbContext)
    {
      _dbContext = dbContext;
    }

    /// <summary>
    /// Create New Permission, Or return the existing permission
    /// </summary>
    /// <param name="permission">Permissio Object</param>
    /// <returns>ActionResult - PermissionResultDTO</returns>
    public async Task<ActionResult<PermissionResultDTO>> CreatePermissionAsync(PermissionRequestDTO permission)
    {
      var permissionConverted = PermissionConverter.PermissionRequest(permission);

      // validate if the permission.type is valid
      if (!Enum.IsDefined(typeof(EPermissionType), permissionConverted.Type))
      {
        return new BadRequestObjectResult("Invalid Permission Type");
      }

      var permissionExists = await _dbContext.Permissions.FirstOrDefaultAsync(p => p.Operation == permissionConverted.Operation && p.Type == permissionConverted.Type);

      // If the permission exists, only return the permission
      if (permissionExists != null)
      {
        return new OkObjectResult(PermissionConverter.PermissionResult(permissionConverted));
      }

      await _dbContext.Permissions.AddAsync(permissionConverted);
      await _dbContext.SaveChangesAsync();

      var permissionReturnConverted = PermissionConverter.PermissionResult(permissionConverted);

      return new OkObjectResult(permissionReturnConverted);
    }

    /// <summary>
    /// Get All Permissions
    /// </summary>
    /// <returns>IEnumerable - Permission</returns>
    public async Task<IEnumerable<Permission>> GetPermissionsAsync()
    {
      return await _dbContext.Permissions.ToListAsync();
    }

    /// <summary>
    /// Delete the permission
    /// </summary>
    /// <param name="permission">Permission</param>
    /// <returns>ActionResult - Permission</returns>
    public async Task<ActionResult> DeletePermissionAsync(PermissionRequestDTO permission)
    {
      var permissionConverted = PermissionConverter.PermissionRequest(permission);

      // validate if operation or type is valid
      if (!Enum.IsDefined(typeof(EPermissionOperation), permissionConverted.Operation) || !Enum.IsDefined(typeof(EPermissionType), permissionConverted.Type))
      {
        return new BadRequestObjectResult("Invalid 'Permission Operation' or 'Type'");
      }

      var permissionExists = await _dbContext.Permissions.FirstOrDefaultAsync(p => p.Operation == permissionConverted.Operation && p.Type == permissionConverted.Type);
      if (permissionExists != null)
      {
        return new BadRequestObjectResult("Permission not found");
      }

      _dbContext.Permissions.Remove(permissionConverted);

      await _dbContext.SaveChangesAsync();

      return new OkObjectResult(permission);
    }

  }
}
