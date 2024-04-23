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
    /// Create New Permission
    /// </summary>
    /// <param name="permission">Permissio Object</param>
    /// <returns></returns>
    public async Task<ActionResult<PermissionResultDTO>> CreatePermissionAsync(PermissionRequestDTO permission)
    {
      var permissionConverted = PermissionConverter.PermissionRequest(permission);
    
      var permissionExists = await _dbContext.Permissions.FirstOrDefaultAsync(p => p.Operation == permissionConverted.Operation && p.Type == permissionConverted.Type);

      if (permissionExists != null)
      {
        return new OkObjectResult(PermissionConverter.PermissionResult(permissionConverted));
      }

      await _dbContext.Permissions.AddAsync(permissionConverted);
      await _dbContext.SaveChangesAsync();

      var newPermission = PermissionConverter.PermissionResult(permissionConverted);

      return new OkObjectResult(newPermission);
    }

    /// <summary>
    /// Get Permission by Operation
    /// </summary>
    /// <param name="operation">Operatio Name</param>
    /// <returns></returns>
    public async Task<ActionResult<PermissionResultDTO>> GetPermissionByOperation(string operation)
    {
      if (!Enum.IsDefined(typeof(EPermissionOperation), operation))
      {
        return new BadRequestResult();
      }

      var operationEnum = (EPermissionOperation)Enum.Parse(typeof(EPermissionOperation), operation);

      var permission = await _dbContext.Permissions.FirstOrDefaultAsync(p => p.Operation == operationEnum);
      if (permission == null)
      {
        return new NotFoundResult(); // Retorna 404 se a permissão não for encontrada
      }

      var permissionDto = PermissionConverter.PermissionResult(permission);
      return new OkObjectResult(permissionDto);
    }

    /// <summary>
    /// Get All Permissions
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Permission>> GetPermissionsAsync()
    {
      return await _dbContext.Permissions.ToListAsync();
    }

    /// <summary>
    /// Update Permission
    /// </summary>
    /// <param name="permission"></param>
    /// <returns></returns>
    public async Task<Permission> UpdatePermissionAsync(Permission permission)
    {
      // get the permission
      var permissionExists = await _dbContext.Permissions.FirstOrDefaultAsync(p => p.Operation == permission.Operation);
      if (permissionExists == null)
      {
        return null;
      }

      // update the permission
      permissionExists.Operation = permission.Operation;
      permissionExists.Type = permission.Type;

      _dbContext.Permissions.Update(permissionExists);
      await _dbContext.SaveChangesAsync();
      return permission;
    }

  }
}
