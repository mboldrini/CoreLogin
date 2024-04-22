using CoreLogin.Domain.Entities.Enum;
using CoreLogin_Domain.Converters.DTO;
using CoreLogin_Domain.Entities;

namespace CoreLogin_Domain.Converters
{
  public class PermissionConverter
  {

    public static PermissionResultDTO PermissionResult(Permission permission)
    {
      return new PermissionResultDTO
      {
        Type = permission.Type.ToString(),
        Operation = permission.Operation.ToString()
      };   
    }

    public static Permission PermissionRequest(PermissionRequestDTO permissionRequest)
    {
      return new Permission
      {
        Type = (EPermissionType)System.Enum.Parse(typeof(EPermissionType), permissionRequest.Type),
        Operation = (EPermissionOperation)System.Enum.Parse(typeof(EPermissionOperation), permissionRequest.Operation)
      };
    }

  }
}
