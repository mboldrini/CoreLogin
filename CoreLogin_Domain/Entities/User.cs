using CoreLogin_Domain.Entities.Relations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CoreLogin_Domain.Entities
{
  public class User
  {
    [Key]
    [JsonIgnore]
    public int Id { get; set; }

    [JsonIgnore]
    public Guid Uid { get; set; }

    [Required(ErrorMessage = "This field is required")]
    [MaxLength(40, ErrorMessage = "This field must have a maximum of 40 characters")]
    [MinLength(3, ErrorMessage = "This field must have a minimum of 3 characters")]
    [DefaultValue("administrator")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "This field is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    [DefaultValue("admin@adminn.com")]
    public string Email { get; set; }

    [Required(ErrorMessage = "This field is required")]
    [MaxLength(254, ErrorMessage = "This field must have a maximum of 40 characters")]
    [MinLength(8, ErrorMessage = "This field must have a minimum of 8 characters")]
    [DefaultValue("admin123")]
    public string Password { get; set; }

    [JsonIgnore]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [JsonIgnore]
    public ICollection<UserGroup> UserGroups { get; set; }

  }
}
