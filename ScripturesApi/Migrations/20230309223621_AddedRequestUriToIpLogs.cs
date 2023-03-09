using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScripturesApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedRequestUriToIpLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RequestUri",
                table: "IpLogs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestUri",
                table: "IpLogs");
        }
    }
}
