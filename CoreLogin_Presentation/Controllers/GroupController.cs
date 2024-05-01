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
      if (newGroup == null)
      {
        return BadRequest("Group already exists");
      }

      return new OkObjectResult(newGroup);
    }


    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<IEnumerable<GroupResultDTO>>> GetAllGroupsAsync()
    {
      var groups = await _groupRepository.GetAllGroupsAsync();

      return new OkObjectResult(groups);
    }

    [HttpGet]
    [Route("id/{id}")]
    public async Task<ActionResult<GroupResultDTO>> GetGroupByIdAsync(int id)
    {
      var group = await _groupRepository.GetGroupByIdAsync(id);
      if(group == null)
      {
        return NotFound();
      }

      return new OkObjectResult(group);
    }

    [HttpGet]
    [Route("name/{name}")]
    public async Task<ActionResult<GroupResultDTO>> GetGroupByNameAndAllPermissionsAsync(string name)
    {
      var group = await _groupRepository.GetGroupByNameAndAllPermissionsAsync(name);
      if (group == null)
      {
        return NotFound();
      }

      return new OkObjectResult(group);
    }

    [HttpPost]
    [Route("update/{id:int}")]
    public async Task<ActionResult<GroupResultDTO>> UpdateGroupAsync([FromRoute] int id, [FromBody] GroupRequestDTO group)
    {
      var updatedGroup = await _groupRepository.UpdateGroupAsync(id, group);
      if (updatedGroup == null)
      {
        return NotFound();
      }

      return new OkObjectResult(updatedGroup);
    }

    [HttpDelete]
    [Route("delete/{id:int}")]
    public async Task<ActionResult<GroupResultDTO>> DeleteGroup([FromRoute] int id)
    {
      var deletedGroup = await _groupRepository.DeleteGroup(id);
      if (deletedGroup == false)
      {
        return new NotFoundObjectResult("Group not found");
      }

      return Ok();
    }

  }
}
