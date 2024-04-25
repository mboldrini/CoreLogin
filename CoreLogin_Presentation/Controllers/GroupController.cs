using CoreLogin_Domain.Converters.DTO;
using CoreLogin_Domain.Entities;
using CoreLogin_Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoreLogin_Presentation.Controllers
{

  [Route("v1/group")]
  public class GroupController : ControllerBase
  {
    private readonly IGroupRepository _groupRepository;

    public GroupController(IGroupRepository groupRepository)
    {
      _groupRepository = groupRepository;
    }

    [HttpPost]
    [Route("create")]
    public async Task<ActionResult<GroupResultDTO>> CreateGroupAsync([FromBody] GroupRequestDTO group)
    {
      var newGroup = await _groupRepository.CreateGroupAsync(group);
      
      return newGroup;
    }


    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<IEnumerable<GroupResultDTO>>> GetAllGroupsAsync()
    {
      var groups = await _groupRepository.GetAllGroupsAsync();

      return groups;
    }

    [HttpGet]
    [Route("id/{id}")]
    public async Task<ActionResult<GroupResultDTO>> GetGroupByIdAsync(int id)
    {
      var group = await _groupRepository.GetGroupByIdAsync(id);

      return group;
    }

    [HttpGet]
    [Route("name/{name}")]
    public async Task<ActionResult<GroupResultDTO>> GetGroupByNameAndAllPermissionsAsync(string name)
    {
      var group = await _groupRepository.GetGroupByNameAndAllPermissionsAsync(name);

      return group;
    }

    [HttpPost]
    [Route("update/{id:int}")]
    public async Task<ActionResult<GroupResultDTO>> UpdateGroupAsync([FromRoute] int id, [FromBody] GroupRequestDTO group)
    {
      var updatedGroup = await _groupRepository.UpdateGroupAsync(id, group);

      return updatedGroup;
    }

    [HttpDelete]
    [Route("delete/{id:int}")]
    public async Task<ActionResult<GroupResultDTO>> DeleteGroup([FromRoute] int id)
    {
      var deletedGroup = await _groupRepository.DeleteGroup(id);

      return deletedGroup;
    }

  }
}
