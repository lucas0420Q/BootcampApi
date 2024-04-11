using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AccountTableModifiedMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SavingAccounts_AccountId",
                table: "SavingAccounts");

            migrationBuilder.DropIndex(
                name: "IX_CurrentAccount_AccountId",
                table: "CurrentAccount");

            migrationBuilder.RenameColumn(
                name: "ExpirationDT",
                table: "CreditCards",
                newName: "ExpirationDate");

            migrationBuilder.CreateIndex(
                name: "IX_SavingAccounts_AccountId",
                table: "SavingAccounts",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CurrentAccount_AccountId",
                table: "CurrentAccount",
                column: "AccountId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SavingAccounts_AccountId",
                table: "SavingAccounts");

            migrationBuilder.DropIndex(
                name: "IX_CurrentAccount_AccountId",
                table: "CurrentAccount");

            migrationBuilder.RenameColumn(
                name: "ExpirationDate",
                table: "CreditCards",
                newName: "ExpirationDT");

            migrationBuilder.CreateIndex(
                name: "IX_SavingAccounts_AccountId",
                table: "SavingAccounts",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentAccount_AccountId",
                table: "CurrentAccount",
                column: "AccountId");
        }
    }
}
