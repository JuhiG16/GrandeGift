using Microsoft.EntityFrameworkCore.Migrations;

namespace GrandeGift.Migrations
{
    public partial class CustomerUSerOneToOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblAdmin_AspNetUsers_UserIdId",
                table: "tblAdmin");

            migrationBuilder.DropIndex(
                name: "IX_tblAdmin_UserIdId",
                table: "tblAdmin");

            migrationBuilder.DropColumn(
                name: "UserIdId",
                table: "tblAdmin");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "tblAdmin",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tblCustomer_UserId",
                table: "tblCustomer",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblAdmin_UserId",
                table: "tblAdmin",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tblAdmin_AspNetUsers_UserId",
                table: "tblAdmin",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tblCustomer_AspNetUsers_UserId",
                table: "tblCustomer",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblAdmin_AspNetUsers_UserId",
                table: "tblAdmin");

            migrationBuilder.DropForeignKey(
                name: "FK_tblCustomer_AspNetUsers_UserId",
                table: "tblCustomer");

            migrationBuilder.DropIndex(
                name: "IX_tblCustomer_UserId",
                table: "tblCustomer");

            migrationBuilder.DropIndex(
                name: "IX_tblAdmin_UserId",
                table: "tblAdmin");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "tblAdmin");

            migrationBuilder.AddColumn<int>(
                name: "UserIdId",
                table: "tblAdmin",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblAdmin_UserIdId",
                table: "tblAdmin",
                column: "UserIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblAdmin_AspNetUsers_UserIdId",
                table: "tblAdmin",
                column: "UserIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
