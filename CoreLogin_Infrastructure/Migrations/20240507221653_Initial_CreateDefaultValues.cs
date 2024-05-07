using CoreLogin.Domain.Entities.Enum;
using CoreLogin_Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreLogin_Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class Initial_CreateDefaultValues : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {

      // Initial Permissions
      migrationBuilder.InsertData(
          table: "Permissions",
          columns: new[] { "Id", "Operation" },
          values: new object[,]
          {
                    { 1, (int)EPermissionOperation.Create },
                    { 2, (int)EPermissionOperation.Read },
                    { 3, (int)EPermissionOperation.Update },
                    { 4, (int)EPermissionOperation.Delete }
          });

      // Initial group - admiinstrators
      migrationBuilder.InsertData(
       table: "Groups",
       columns: new[] { "Id", "Name", "Description", "Active", "CanDelete", "Created_At", "Updated_At" },
       values: new object[] { 1, "Administrators", "Default group for administrators", true, false, DateTime.Now, DateTime.Now }
      );

      // Group Permissions - Administrators
      migrationBuilder.InsertData(
          table: "GroupPermissions",
          columns: new[] { "GroupId", "PermissionId" },
          values: new object[,]
          {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 }
          }
      );

      // Add default user
      migrationBuilder.InsertData(
          table: "Users",
          columns: new[] { "Id", "Uid", "UserName", "Email", "Password", "CreatedAt" },
          values: new object[] { 1, Guid.NewGuid(), "administrator", "admin@admin.com", PasswordHelper.CriptographPassword("admin123"), DateTime.Now }
      );

      // Add default user to group
      migrationBuilder.InsertData(
          table: "UserGroups",
          columns: new[] { "UserId", "GroupId" },
          values: new object[] { 1, 1 }
      );

      // Default module
      migrationBuilder.InsertData(
          table: "Modules",
          columns: new[] { "Id", "Name", "Description", "Active", "Created_at", "Updated_at" },
          values: new object[] { 1, "Configuration", "Default module for general configurations", true, DateTime.Now, DateTime.Now }
      );

      // Default group module
      migrationBuilder.InsertData(
          table: "GroupModules",
          columns: new[] { "GroupId", "ModuleId" },
          values: new object[] { 1, 1 }
      );

    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DeleteData(
          table: "GroupModules",
          keyColumns: new[] { "GroupId", "ModuleId" },
          keyValues: new object[] { 1, 1 }
      );

      migrationBuilder.DeleteData(
          table: "Modules",
          keyColumn: "Id",
          keyValue: 1
      );

      migrationBuilder.DeleteData(
          table: "UserGroups",
          keyColumns: new[] { "UserId", "GroupId" },
          keyValues: new object[] { 1, 1 }
      );

      migrationBuilder.DeleteData(
          table: "Users",
          keyColumn: "Id",
          keyValue: 1
      );

      migrationBuilder.DeleteData(
          table: "GroupPermissions",
          keyColumns: new[] { "GroupId", "PermissionId" },
          keyValues: new object[] { 1, 1 }
      );
      migrationBuilder.DeleteData(
          table: "GroupPermissions",
          keyColumns: new[] { "GroupId", "PermissionId" },
          keyValues: new object[] { 1, 2 }
      );
      migrationBuilder.DeleteData(
          table: "GroupPermissions",
          keyColumns: new[] { "GroupId", "PermissionId" },
          keyValues: new object[] { 1, 3 }
      );
      migrationBuilder.DeleteData(
          table: "GroupPermissions",
          keyColumns: new[] { "GroupId", "PermissionId" },
          keyValues: new object[] { 1, 4 }
      );

      migrationBuilder.DeleteData(
          table: "Groups",
          keyColumn: "Id",
          keyValue: 1
      );

      migrationBuilder.DeleteData(
          table: "Permissions",
          keyColumn: "Id",
          keyValue: 1
      );
      migrationBuilder.DeleteData(
          table: "Permissions",
          keyColumn: "Id",
          keyValue: 2
      );
      migrationBuilder.DeleteData(
          table: "Permissions",
          keyColumn: "Id",
          keyValue: 3
      );
      migrationBuilder.DeleteData(
          table: "Permissions",
          keyColumn: "Id",
          keyValue: 4
      );
    }
  }
}
