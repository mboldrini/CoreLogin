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
    public async Task<PermissionResultDTO> CreatePermissionAsync(PermissionRequestDTO permission)
    {
      var permissionConverted = PermissionConverter.PermissionRequest(permission);

      var permissionExists = await _dbContext.Permissions.FirstOrDefaultAsync(p => p.Operation == permissionConverted.Operation && p.Type == permissionConverted.Type);

      // If the permission exists, only return the permission
      if (permissionExists != null)
      {
        return PermissionConverter.PermissionResult(permissionConverted);
      }

      await _dbContext.Permissions.AddAsync(permissionConverted);
      await _dbContext.SaveChangesAsync();

      var permissionReturnConverted = PermissionConverter.PermissionResult(permissionConverted);

      return permissionReturnConverted;
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
    public async Task<PermissionResultDTO> DeletePermissionAsync(PermissionRequestDTO permission)
    {
      var permissionConverted = PermissionConverter.PermissionRequest(permission);

      var permissionExists = await _dbContext.Permissions.FirstOrDefaultAsync(p => p.Operation == permissionConverted.Operation && p.Type == permissionConverted.Type);
      if (permissionExists != null)
      {
        return null;
      }

      _dbContext.Permissions.Remove(permissionConverted);

      await _dbContext.SaveChangesAsync();

      return PermissionConverter.PermissionResult(permissionExists);
    }

  }
}
