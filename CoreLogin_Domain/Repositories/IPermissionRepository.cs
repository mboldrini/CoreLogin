using CoreLogin_Domain.Converters.DTO;
using CoreLogin_Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CoreLogin_Domain.Repositories
{
  public interface IPermissionRepository
  {
    Task<ActionResult<PermissionResultDTO>> CreatePermissionAsync(PermissionRequestDTO permission);
    Task<IEnumerable<Permission>> GetPermissionsAsync();
    Task<ActionResult<PermissionResultDTO>> GetPermissionByOperation(string operation);
    Task<Permission> UpdatePermissionAsync(Permission permission);
  }
}
