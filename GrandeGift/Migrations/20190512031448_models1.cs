using Microsoft.EntityFrameworkCore.Migrations;

namespace GrandeGift.Migrations
{
    public partial class models1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "tblAdmin",
                newName: "UserIdId");

            migrationBuilder.RenameIndex(
                name: "IX_tblAdmin_UserId",
                table: "tblAdmin",
                newName: "IX_tblAdmin_UserIdId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "tblCustomer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tblAdmin_AspNetUsers_UserIdId",
                table: "tblAdmin",
                column: "UserIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblAdmin_AspNetUsers_UserIdId",
                table: "tblAdmin");

            migrationBuilder.RenameColumn(
                name: "UserIdId",
                table: "tblAdmin",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_tblAdmin_UserIdId",
                table: "tblAdmin",
                newName: "IX_tblAdmin_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "tblCustomer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_tblCustomer_UserId",
                table: "tblCustomer",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblAdmin_AspNetUsers_UserId",
                table: "tblAdmin",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblCustomer_AspNetUsers_UserId",
                table: "tblCustomer",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
