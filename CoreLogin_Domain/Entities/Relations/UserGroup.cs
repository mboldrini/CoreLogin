using System.ComponentModel.DataAnnotations;

namespace CoreLogin_Domain.Entities.Relations
{
  /// <summary>
  /// UserGroup entity - User and Group Tables
  /// </summary>
  public class UserGroup
  {
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public int GroupId { get; set; }
    public Group Group { get; set; }
  }
}
