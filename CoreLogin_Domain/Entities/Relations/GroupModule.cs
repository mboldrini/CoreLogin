using System.ComponentModel.DataAnnotations;

namespace CoreLogin_Domain.Entities.Relations
{
  public  class GroupModule
  {
    [Key]
    public int Id { get; set; }

    public int GroupId { get; set; }
    public Group Group { get; set; }

    public int ModuleId { get; set; }
    public Module Module { get; set; }

  }
}
