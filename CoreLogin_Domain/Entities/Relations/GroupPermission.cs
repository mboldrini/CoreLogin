using System.ComponentModel.DataAnnotations;

namespace CoreLogin_Domain.Entities.Relations
{
  /// <summary>
  /// GroupPermission entity - Group and Permission Tables
  /// </summary>
  public class GroupPermission
  {
    [Key]
    public int Id { get; set; }

    public int GroupId { get; set; }
    public Group Group { get; set; }

    public int PermissionId { get; set; }
    public Permission Permission { get; set; }

  }
}
