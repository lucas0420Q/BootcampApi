using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Request6Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Requests",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "ProductType",
                table: "Requests",
                newName: "Status");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Requests",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "Request_pkey",
                table: "Requests",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ProductId",
                table: "Requests",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Product_ProductId",
                table: "Requests",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Product_ProductId",
                table: "Requests");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "Request_pkey",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_ProductId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Requests",
                newName: "ProductType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Requests",
                table: "Requests",
                column: "Id");
        }
    }
}
