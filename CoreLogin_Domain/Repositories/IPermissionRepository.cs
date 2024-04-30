using CoreLogin_Domain.Converters.DTO;
using CoreLogin_Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CoreLogin_Domain.Repositories
{
  public interface IPermissionRepository
  {
    Task<PermissionResultDTO> CreatePermissionAsync(PermissionRequestDTO permission);
    Task<IEnumerable<Permission>> GetPermissionsAsync();
    Task<PermissionResultDTO> DeletePermissionAsync(PermissionRequestDTO permission);
  }
}
