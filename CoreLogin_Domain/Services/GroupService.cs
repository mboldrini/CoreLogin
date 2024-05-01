using CoreLogin_Domain.Converters.DTO;
using CoreLogin_Domain.Repositories;

namespace CoreLogin_Application.Services
{
  public class GroupService
  {

    public readonly IGroupRepository _groupRepository;
    public GroupService(IGroupRepository groupRepository)
    {
      _groupRepository = groupRepository;
    }

    public async Task<GroupResultDTO> GetGroupByIdAsync(int groupId)
    {
      return await _groupRepository.GetGroupByIdAsync(groupId);
    }

    public async Task<GroupResultDTO> GetGroupByNameAndAllPermissionsAsync(string groupName)
    {
      return await _groupRepository.GetGroupByNameAndAllPermissionsAsync(groupName);
    }

    public async Task<IEnumerable<GroupResultDTO>> GetGroupsAsync()
    {
      return await _groupRepository.GetAllGroupsAsync();
    }

    public async Task<GroupResultDTO> CreateGroupAsync(GroupRequestDTO group)
    {
      return await _groupRepository.CreateGroupAsync(group);
    }

    public async Task<GroupResultDTO> UpdateGroupAsync(int id, GroupRequestDTO group)
    {
      return await _groupRepository.UpdateGroupAsync(id, group);
    }

  }
}
