using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Deposit2Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deposits_BankDTO_BankId",
                table: "Deposits");

            migrationBuilder.DropTable(
                name: "BankDTO");

            migrationBuilder.AddForeignKey(
                name: "FK_Deposits_Bank_BankId",
                table: "Deposits",
                column: "BankId",
                principalTable: "Bank",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deposits_Bank_BankId",
                table: "Deposits");

            migrationBuilder.CreateTable(
                name: "BankDTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Mail = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankDTO", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Deposits_BankDTO_BankId",
                table: "Deposits",
                column: "BankId",
                principalTable: "BankDTO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
