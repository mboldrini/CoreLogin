using CoreLogin.Domain.Entities.Enum;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CoreLogin_Domain.Entities
{
  public class Permission
  {
    [Key]
    [JsonIgnore]
    public int Id { get; set; }

    [Required]
    [DefaultValue(0)]
    public EPermissionType Type { get; set; }

    [Required]
    [DefaultValue(0)]
    public EPermissionOperation Operation { get; set; }


  }
}

