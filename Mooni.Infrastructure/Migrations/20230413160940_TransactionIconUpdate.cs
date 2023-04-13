using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mooni.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TransactionIconUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconUrl",
                table: "Transactions");

            migrationBuilder.AddColumn<int>(
                name: "IconId",
                table: "Transactions",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconId",
                table: "Transactions");

            migrationBuilder.AddColumn<string>(
                name: "IconUrl",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
