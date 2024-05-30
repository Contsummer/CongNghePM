using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caffe.Migrations
{
    /// <inheritdoc />
    public partial class addDrinkDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "descripton",
                table: "Drinks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "descripton",
                table: "Drinks");
        }
    }
}
