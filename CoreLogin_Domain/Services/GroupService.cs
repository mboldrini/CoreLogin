using CoreLogin_Domain.Converters.DTO;
using CoreLogin_Domain.Entities;
using CoreLogin_Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoreLogin_Application.Services
{
  public class GroupService
  {

    public readonly IGroupRepository _groupRepository;
    public GroupService(IGroupRepository groupRepository)
    {
      _groupRepository = groupRepository;
    }

    public async Task<ActionResult<GroupResultDTO>> GetGroupByIdAsync(int groupId)
    {
      return await _groupRepository.GetGroupByIdAsync(groupId);
    }

    public async Task<ActionResult<GroupResultDTO>> GetGroupByNameAndAllPermissionsAsync(string groupName)
    {
      return await _groupRepository.GetGroupByNameAndAllPermissionsAsync(groupName);
    }

    public async Task<ActionResult<IEnumerable<GroupResultDTO>>> GetGroupsAsync()
    {
      return await _groupRepository.GetAllGroupsAsync();
    }

    public async Task<ActionResult<GroupResultDTO>> CreateGroupAsync(GroupRequestDTO group)
    {
      return await _groupRepository.CreateGroupAsync(group);
    }

    public async Task<ActionResult<GroupResultDTO>> UpdateGroupAsync(int id, GroupRequestDTO group)
    {
      return await _groupRepository.UpdateGroupAsync(id, group);
    }

  }
}
