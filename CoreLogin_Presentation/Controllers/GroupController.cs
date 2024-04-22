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
    [Route("get/id/{id}")]
    public async Task<ActionResult<Group>> GetGroupByIdAsync(int id)
    {
      var group = await _groupRepository.GetGroupByIdAsync(id);

      return group;
    }

    [HttpGet]
    [Route("get/name/{name}")]
    public async Task<ActionResult<Group>> GetGroupByNameAndAllPermissionsAsync(string name)
    {
      var group = await _groupRepository.GetGroupByNameAndAllPermissionsAsync(name);

      return group;
    }

    [HttpPost]
    [Route("update")]
    public async Task<ActionResult<Group>> UpdateGroupAsync([FromBody] Group group)
    {
      var updatedGroup = await _groupRepository.UpdateGroupAsync(group);

      return updatedGroup;
    }

  }
}
