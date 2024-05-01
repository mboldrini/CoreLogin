using CoreLogin_Domain.Converters.DTO;
using CoreLogin_Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CoreLogin_Domain.Repositories
{
  public interface IGroupRepository
  {
    Task<GroupResultDTO> CreateGroupAsync(GroupRequestDTO group);
    Task<GroupResultDTO> GetGroupByIdAsync(int id);
    Task<GroupResultDTO> GetGroupByNameAndAllPermissionsAsync(string name);
    Task<IEnumerable<GroupResultDTO>> GetAllGroupsAsync();
    Task<GroupResultDTO> UpdateGroupAsync(int id, GroupRequestDTO group);
    Task<bool> DeleteGroup(int id);
  }
}
