using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Promotion2Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationTime",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "PercentageOff",
                table: "Promotions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DurationTime",
                table: "Promotions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PercentageOff",
                table: "Promotions",
                type: "text",
                nullable: true);
        }
    }
}
