using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoeStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class changedSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "Sneakers");

            migrationBuilder.AddColumn<string>(
                name: "Sizes",
                table: "Sneakers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sizes",
                table: "Sneakers");

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "Sneakers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
