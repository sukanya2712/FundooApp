using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class NewCollab : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collabs_Collabs_NoteId",
                table: "Collabs");

            migrationBuilder.DropForeignKey(
                name: "FK_Collabs_Collabs_userID",
                table: "Collabs");

            migrationBuilder.AddForeignKey(
                name: "FK_Collabs_Notes_NoteId",
                table: "Collabs",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "NoteId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Collabs_Users_userID",
                table: "Collabs",
                column: "userID",
                principalTable: "Users",
                principalColumn: "userID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collabs_Notes_NoteId",
                table: "Collabs");

            migrationBuilder.DropForeignKey(
                name: "FK_Collabs_Users_userID",
                table: "Collabs");

            migrationBuilder.AddForeignKey(
                name: "FK_Collabs_Collabs_NoteId",
                table: "Collabs",
                column: "NoteId",
                principalTable: "Collabs",
                principalColumn: "collabId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Collabs_Collabs_userID",
                table: "Collabs",
                column: "userID",
                principalTable: "Collabs",
                principalColumn: "collabId",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
