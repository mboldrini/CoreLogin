using CoreLogin_Domain.Entities.Relations;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CoreLogin_Domain.Entities
{
  public class Group
  {
    [Key]
    [SwaggerSchema(ReadOnly = true)]
    public int Id { get; set; }

    [Required(ErrorMessage = "This field is required")]
    [MaxLength(40, ErrorMessage = "This field must have a maximum of 40 characters")]
    [MinLength(3, ErrorMessage = "This field must have a minimum of 3 characters")]
    [DefaultValue("Admins")]
    public string Name { get; set; }

    [DefaultValue("Default group for administrators")]
    public string? Description { get; set; }

    [DefaultValue(true)]
    public bool Active { get; set; } = true;

    public ICollection<UserGroup> UserGroups { get; set; }

    public ICollection<GroupModule> GroupModules { get; set; }

    public ICollection<GroupPermission> GroupPermissions { get; set; }

  }
}
