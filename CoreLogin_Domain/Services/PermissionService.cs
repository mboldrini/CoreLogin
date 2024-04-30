using CoreLogin_Domain.Converters.DTO;
using CoreLogin_Domain.Entities;
using CoreLogin_Domain.Repositories;
using CoreLogin_Domain.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CoreLogin_Application.Services
{
  public class PermissionService
  {
    public readonly IPermissionRepository _permissionRepository;

    public PermissionService(IPermissionRepository permissionRepository)
    {
      _permissionRepository = permissionRepository;
    }

    public async Task<IEnumerable<Permission>> GetPermissionsAsync()
    {
      return await _permissionRepository.GetPermissionsAsync();
    }

    public async Task<ActionResult<PermissionResultDTO>> CreatePermissionAsync(PermissionRequestDTO permission)
    {
      return await _permissionRepository.CreatePermissionAsync(permission);
    }

    public async Task<PermissionResultDTO> DeletePermissionAsync(PermissionRequestDTO permission)
    {
      return await _permissionRepository.DeletePermissionAsync(permission);
    }

  }
}
