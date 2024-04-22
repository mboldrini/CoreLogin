using CoreLogin_Domain.Converters.DTO;
using CoreLogin_Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CoreLogin_Domain.Repositories
{
  public interface IGroupRepository
  {
    Task<ActionResult<GroupResultDTO>> CreateGroupAsync(GroupRequestDTO group);
    Task<ActionResult<Group>> GetGroupByIdAsync(int id);
    Task<ActionResult<Group>> GetGroupByNameAndAllPermissionsAsync(string name);
    Task<ActionResult<IEnumerable<Group>>> GetGroupsAsync();
    Task<ActionResult<Group>> UpdateGroupAsync(Group group);
  }
}
