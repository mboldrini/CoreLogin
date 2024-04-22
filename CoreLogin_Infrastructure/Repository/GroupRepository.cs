using CoreLogin.Domain.Entities.Enum;
using CoreLogin_Domain.Converters;
using CoreLogin_Domain.Converters.DTO;
using CoreLogin_Domain.Entities;
using CoreLogin_Domain.Entities.Relations;
using CoreLogin_Domain.Repositories;
using CoreLogin_Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace CoreLogin_Infrastructure.Repository
{
  public class GroupRepository : IGroupRepository
  {
    public readonly DataContext _dbContext;

    public GroupRepository(DataContext dbContext)
    {
      _dbContext = dbContext;
    }

    /// <summary>
    /// Create a new group
    /// </summary>
    /// <param name="group">Group</param>
    /// <returns></returns>
    public async Task<ActionResult<GroupResultDTO>> CreateGroupAsync(GroupRequestDTO group)
    {
      var groupExist = await _dbContext.Groups.FirstOrDefaultAsync(g => g.Name == group.Name);
      if (groupExist != null)
      {
        return new ConflictObjectResult("Group Already Exist");
      }

      var newGroup = new Group
      {
        Name = group.Name,
        Description = group.Description,
        Active = group.Active
      };

      await _dbContext.Groups.AddAsync(newGroup);

      var permissionDTOs = new List<PermissionResultDTO>();

      foreach (var permission in group.Permissions)
      {
        var permissionOperation = (EPermissionOperation)Enum.Parse(typeof(EPermissionOperation), permission.Operation);

        var existingPermission = await _dbContext.Permissions.FirstOrDefaultAsync(p => p.Operation == permissionOperation);

        if (existingPermission != null)
        {
          var groupPermission = new GroupPermission
          {
            GroupId = newGroup.Id,
            Group = newGroup,
            PermissionId = existingPermission.Id,
            Permission = existingPermission
          };

          await _dbContext.GroupPermissions.AddAsync(groupPermission);

          var permissionDTO = new PermissionResultDTO
          {
            Type = existingPermission.Type.ToString(),
            Operation = existingPermission.Operation.ToString()
          };

          permissionDTOs.Add(permissionDTO);
        }
      }

      await _dbContext.SaveChangesAsync();

      var groupDTO = GroupConverter.GroupResult(newGroup);
      return new OkObjectResult(groupDTO);


    }

    /// <summary>
    /// Get a group by Id
    /// </summary>
    /// <param name="groupId">ID</param>
    /// <returns></returns>
    public async Task<ActionResult<Group>> GetGroupByIdAsync(int id)
    {
      var groupExist = await _dbContext.Groups
                    .Include(g => g.GroupPermissions)
                        .ThenInclude(gp => gp.Permission)
                    .FirstOrDefaultAsync(g => g.Id == id);

      if (groupExist == null)
      {
        return new NotFoundResult();
      }

      var groupConverted = GroupConverter.GroupResult(groupExist);

      return new OkObjectResult(groupConverted);
    }

    /// <summary>
    /// Get a group by name
    /// </summary>
    /// <param name="name">Name</param>
    /// <returns></returns>
    public async Task<ActionResult<Group>> GetGroupByNameAndAllPermissionsAsync(string name)
    {
      var groupExist = await _dbContext.Groups
                    .Include(g => g.GroupPermissions)
                        .ThenInclude(gp => gp.Permission)
                    .FirstOrDefaultAsync(g => g.Name == name);

      if (groupExist == null)
      {
        return new NotFoundResult();
      }

      var groupConverted = GroupConverter.GroupResult(groupExist);

      return new OkObjectResult(groupConverted);

    }

    /// <summary>
    /// Return a list of groups
    /// </summary>
    /// <returns></returns>
    public async Task<ActionResult<IEnumerable<Group>>> GetGroupsAsync()
    {
      var listOfGroups = await _dbContext.Groups.ToListAsync();
      return new OkObjectResult(listOfGroups);
    }

    /// <summary>
    /// Update a group
    /// </summary>
    /// <param name="group"></param>
    /// <returns></returns>
    public async Task<ActionResult<Group>> UpdateGroupAsync(Group group)
    {
      var groupExist = await _dbContext.Groups.FirstOrDefaultAsync(g => g.Id == group.Id);
      if (groupExist == null)
      {
        return new OkObjectResult(groupExist);
      }

      groupExist.Name = group.Name;
      groupExist.Description = group.Description;
      groupExist.Active = group.Active;

      _dbContext.Groups.Update(groupExist);

      await _dbContext.SaveChangesAsync();

      return new OkObjectResult(groupExist);

    }
  }
}
