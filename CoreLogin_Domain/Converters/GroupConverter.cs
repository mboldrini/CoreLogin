using CoreLogin_Domain.Converters.DTO;
using CoreLogin_Domain.Entities;

namespace CoreLogin_Domain.Converters
{
  public class GroupConverter
  {

    public static GroupResultDTO GroupResult(Group group)
    {

      return new GroupResultDTO
      {
        Id = group.Id,
        Name = group.Name,
        Description = group.Description,
        Active = group.Active,
        Permissions = group.GroupPermissions.Select(gp => PermissionConverter.PermissionResult(gp.Permission))
      };
    }

    public static Group GroupRequest(GroupRequestDTO groupRequest)
    {
      return new Group
      {
        Name = groupRequest.Name,
        Description = groupRequest.Description,
        Active = groupRequest.Active,
      };
    }

  }
}
