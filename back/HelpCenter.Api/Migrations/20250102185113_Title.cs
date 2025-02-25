using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetCore.Identity.Migrations
{
    /// <inheritdoc />
    public partial class Title : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "identity",
                table: "Events",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                schema: "identity",
                table: "Events",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "identity",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Title",
                schema: "identity",
                table: "Events");
        }
    }
}
