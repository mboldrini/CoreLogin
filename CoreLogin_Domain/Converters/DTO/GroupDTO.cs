using CoreLogin.Domain.Entities.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using CoreLogin_Domain.Entities.Relations;
using CoreLogin_Domain.Entities;

namespace CoreLogin_Domain.Converters.DTO
{

  public sealed class GroupResultDTO {   
    public int Id { get; set; }
  
    public string Name { get; set; }

    public string Description { get; set; }

    public bool Active { get; set; }

    public IEnumerable<PermissionResultDTO> Permissions { get; set; }
  }

  public sealed class GroupRequestDTO
  {
    [Required(ErrorMessage = "This field is required")]
    [MaxLength(40, ErrorMessage = "This field must have a maximum of 40 characters")]
    [MinLength(3, ErrorMessage = "This field must have a minimum of 3 characters")]
    [DefaultValue("Admins")]
    public string Name { get; set; }

    [DefaultValue("Default group for administrators")]
    public string? Description { get; set; }

    [DefaultValue(true)]
    public bool Active { get; set; } = true;

    public IEnumerable<PermissionResultDTO> Permissions { get; set; }
  }
}
