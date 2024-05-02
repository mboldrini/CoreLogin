using CoreLogin_Domain.Converters.DTO;
using CoreLogin_Domain.Entities;

namespace CoreLogin_Domain.Converters
{
  public class GroupConverter
  {
    /// <summary>
    /// Convert Group to GroupResultDTO
    /// </summary>
    /// <param name="group">Group</param>
    /// <returns>GroupResultDTO</returns>
    public static GroupResultDTO GroupResult(Group group)
    {
      return new GroupResultDTO
      {
        Id = group.Id,
        Name = group.Name,
        Description = group.Description,
        Active = group.Active,
        CanDelete = group.CanDelete,
        Created_At = group.Created_At ?? DateTime.Now,
        Updated_At = group.Updated_At ?? DateTime.Now,
        Permissions = group.GroupPermissions.Select(gp => PermissionConverter.PermissionResult(gp.Permission))
      };
    }

    /// <summary>
    /// Convert GroupRequestDTO to Group
    /// </summary>
    /// <param name="groupRequest">GroupRequestDTO</param>
    /// <returns>Group</returns>
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
