using CoreLogin_Domain.Entities.Relations;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CoreLogin_Domain.Entities
{
  public class Module
  {
    [Key]
    [SwaggerSchema(ReadOnly = true)]
    public int Id { get; set; }

    [Required(ErrorMessage = "This field is required")]
    [MaxLength(40, ErrorMessage = "This field must have a maximum of 40 characters")]
    [MinLength(3, ErrorMessage = "This field must have a minimum of 3 characters")]
    [DefaultValue("Administrative")]
    public string Name { get; set; }

    [DefaultValue("Example of Module Name - Administrative")]
    public string? Description { get; set; }

    [DefaultValue(true)]
    public bool Active { get; set; } = true;

    [ReadOnly(true)]
    public DateTime Created_at { get; set; } = DateTime.Now;

    [ReadOnly(true)]
    public DateTime Updated_at { get; set; } = DateTime.Now;

    [JsonIgnore]
    public ICollection<GroupModule> GroupModules { get; set; }
  }
}
