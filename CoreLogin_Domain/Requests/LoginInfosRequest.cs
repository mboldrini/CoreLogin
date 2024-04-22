using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CoreLogin_Domain.Requests
{
  public class LoginInfosRequest
  {

    [Required]
    [DefaultValue("admin@admin.com")]
    public string Email { get; set; }

    [Required]
    [DefaultValue("admin123")]
    public string Password { get; set; }
  }
}
