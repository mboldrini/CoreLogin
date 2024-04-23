using CoreLogin_Domain.Converters.DTO;
using CoreLogin_Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CoreLogin_Domain.Repositories
{
  public interface IGroupRepository
  {
    Task<ActionResult<GroupResultDTO>> CreateGroupAsync(GroupRequestDTO group);
    Task<ActionResult<GroupResultDTO>> GetGroupByIdAsync(int id);
    Task<ActionResult<GroupResultDTO>> GetGroupByNameAndAllPermissionsAsync(string name);
    Task<ActionResult<IEnumerable<GroupResultDTO>>> GetAllGroupsAsync();
    Task<ActionResult<GroupResultDTO>> UpdateGroupAsync(int id, GroupRequestDTO group);
    Task<ActionResult> DeleteGroup(int id);
  }
}
