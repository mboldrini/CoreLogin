using CoreLogin.Domain.Entities.Enum;
using CoreLogin_Domain.Converters;
using CoreLogin_Domain.Converters.DTO;
using CoreLogin_Domain.Entities;
using CoreLogin_Domain.Entities.Relations;
using CoreLogin_Domain.Repositories;
using CoreLogin_Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    /// <returns>GroupResultDTO</returns>
    public async Task<GroupResultDTO> CreateGroupAsync(GroupRequestDTO group)
    {
      var groupExist = await _dbContext.Groups.FirstOrDefaultAsync(g => g.Name == group.Name);
      if (groupExist != null)
      {
        return null;
      }

      var newGroup = GroupConverter.GroupRequest(group);

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
            Operation = existingPermission.Operation.ToString()
          };

          permissionDTOs.Add(permissionDTO);
        }
      }

      await _dbContext.SaveChangesAsync();

      return GroupConverter.GroupResult(newGroup);
    }

    /// <summary>
    /// Get a group by Id
    /// </summary>
    /// <param name="groupId">ID</param>
    /// <returns>GroupResultDTO</returns>
    public async Task<GroupResultDTO> GetGroupByIdAsync(int id)
    {
      var groupExist = await _dbContext.Groups
                    .Include(g => g.GroupPermissions)
                        .ThenInclude(gp => gp.Permission)
                    .FirstOrDefaultAsync(g => g.Id == id);

      if (groupExist == null)
      {
        return null;
      }

      return GroupConverter.GroupResult(groupExist);
    }

    /// <summary>
    /// Get a group by name
    /// </summary>
    /// <param name="name">Name</param>
    /// <returns>GroupResultDTO</returns>
    public async Task<GroupResultDTO> GetGroupByNameAndAllPermissionsAsync(string name)
    {
      var groupExist = await _dbContext.Groups
                    .Include(g => g.GroupPermissions)
                        .ThenInclude(gp => gp.Permission)
                    .FirstOrDefaultAsync(g => g.Name == name);

      if (groupExist == null)
      {
        return null;
      }

      return GroupConverter.GroupResult(groupExist);

    }

    /// <summary>
    /// Return a list of groups
    /// </summary>
    /// <returns>IEnumerable - GroupResultDTO</returns>
    public async Task<IEnumerable<GroupResultDTO>> GetAllGroupsAsync()
    {
      // get all groups with related permissions
      var groups = await _dbContext.Groups
                    .Include(g => g.GroupPermissions)
                        .ThenInclude(gp => gp.Permission)
                    .ToListAsync();
     
      // convert list of groupts to groupDTO
      var groupsDTO = new List<GroupResultDTO>();
      foreach (var group in groups)
      {
        var groupDTO = GroupConverter.GroupResult(group);
        groupsDTO.Add(groupDTO);
      }

      return groupsDTO;

    }

    /// <summary>
    /// Update Group and permissions
    /// </summary>
    /// <param name="id">ID of the group</param>
    /// <param name="group">Group infos with permissions</param>
    /// <returns></returns>
    public async Task<GroupResultDTO> UpdateGroupAsync(int id, GroupRequestDTO group)
    {
      var groupExist = await _dbContext.Groups.FirstOrDefaultAsync(g => g.Id == id);
      if (groupExist == null)
      {
        return null;
      }

      var groupPermissions = await _dbContext.GroupPermissions.Where(gp => gp.GroupId == groupExist.Id).ToListAsync();

      _dbContext.GroupPermissions.RemoveRange(groupPermissions);

      groupExist.Name = group.Name;
      groupExist.Description = group.Description;
      groupExist.Active = group.Active;

      foreach (var permission in group.Permissions)
      {
        var permissionOperation = (EPermissionOperation)Enum.Parse(typeof(EPermissionOperation), permission.Operation);

        var existingPermission = await _dbContext.Permissions.FirstOrDefaultAsync(p => p.Operation == permissionOperation);

        if (existingPermission != null)
        {
          var groupPermission = new GroupPermission
          {
            GroupId = groupExist.Id,
            Group = groupExist,
            PermissionId = existingPermission.Id,
            Permission = existingPermission
          };

          await _dbContext.GroupPermissions.AddAsync(groupPermission);
        }
      }

      await _dbContext.SaveChangesAsync();

      return GroupConverter.GroupResult(groupExist);

    }

    /// <summary>
    /// Delete a group
    /// </summary>
    /// <param name="id">ID of the group</param>
    /// <returns>ActionResult</returns>
    public async Task<bool> DeleteGroup(int id)
    {
      var groupExist = await _dbContext.Groups.FirstOrDefaultAsync(g => g.Id == id);
      if (groupExist == null)
      {
        return false;
      }

      // set group active to false and save
      groupExist.Active = false;
      await _dbContext.SaveChangesAsync();  

      return true;
    }

  }
}
