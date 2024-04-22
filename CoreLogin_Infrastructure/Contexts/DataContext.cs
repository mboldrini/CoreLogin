using Microsoft.EntityFrameworkCore;
using CoreLogin_Domain.Entities;
using CoreLogin_Domain.Entities.Relations;

namespace CoreLogin_Infrastructure.Data
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<Module> Modules { get; set; }
    public DbSet<Group> Groups { get; set; }

    public DbSet<UserGroup> UserGroups { get; set; }
    public DbSet<GroupModule> GroupModules { get; set; }
    public DbSet<GroupPermission> GroupPermissions { get; set; }


  }
}
