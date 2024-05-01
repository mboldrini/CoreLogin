using CoreLogin_Domain.Converters.DTO;
using CoreLogin_Domain.Entities;

namespace CoreLogin_Domain.Repositories
{
  public interface IPermissionRepository
  {
    Task<PermissionResultDTO> CreatePermissionAsync(PermissionRequestDTO permission);
    Task<IEnumerable<Permission>> GetPermissionsAsync();
    Task<PermissionResultDTO> DeletePermissionAsync(PermissionRequestDTO permission);
  }
}
