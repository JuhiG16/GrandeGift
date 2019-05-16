using Microsoft.EntityFrameworkCore.Migrations;

namespace GrandeGift.Migrations
{
    public partial class models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "tblCustomer",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "tblAdmin",
                newName: "UserId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "tblCustomer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "tblCustomer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "tblAdmin",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_tblCustomer_UserId",
                table: "tblCustomer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAdmin_UserId",
                table: "tblAdmin",
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
                name: "Gender",
                table: "tblCustomer");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "tblCustomer",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "tblAdmin",
                newName: "UserID");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "tblCustomer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "tblAdmin",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
