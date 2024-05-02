using CoreLogin.Domain.Entities.Enum;
using CoreLogin_Domain.Converters.DTO;
using CoreLogin_Domain.Entities;

namespace CoreLogin_Domain.Converters
{
  public class PermissionConverter
  {
    /// <summary>
    /// Convert Permission to PermissionDTO
    /// </summary>
    /// <param name="permission">Permission</param>
    /// <returns>PermissionResultDTO</returns>
    public static PermissionResultDTO PermissionResult(Permission permission)
    {
      return new PermissionResultDTO
      {
        Operation = permission.Operation.ToString()
      };   
    }

    /// <summary>
    /// Convert PermissionRequestDTO to Permission
    /// </summary>
    /// <param name="permissionRequest">PermissionRequestDTO</param>
    /// <returns>Permission</returns>
    public static Permission PermissionRequest(PermissionRequestDTO permissionRequest)
    {
      return new Permission
      {
        Operation = (EPermissionOperation)System.Enum.Parse(typeof(EPermissionOperation), permissionRequest.Operation)
      };
    }

  }
}
