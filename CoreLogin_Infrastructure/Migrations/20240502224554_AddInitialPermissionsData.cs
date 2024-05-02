using CoreLogin.Domain.Entities.Enum;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreLogin_Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class AddInitialPermissionsData : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
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
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {

    }
  }
}
