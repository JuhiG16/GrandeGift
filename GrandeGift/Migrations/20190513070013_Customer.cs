using Microsoft.EntityFrameworkCore.Migrations;

namespace GrandeGift.Migrations
{
    public partial class Customer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tblCustomer",
                table: "tblCustomer");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "tblCustomer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblCustomer",
                table: "tblCustomer",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCustomer_UserId",
                table: "tblCustomer",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tblCustomer",
                table: "tblCustomer");

            migrationBuilder.DropIndex(
                name: "IX_tblCustomer_UserId",
                table: "tblCustomer");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "tblCustomer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblCustomer",
                table: "tblCustomer",
                column: "UserId");
        }
    }
}
