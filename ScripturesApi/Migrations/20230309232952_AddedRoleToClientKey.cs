using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScripturesApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedRoleToClientKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientKeyRole",
                table: "ClientKeys",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientKeyRole",
                table: "ClientKeys");
        }
    }
}
