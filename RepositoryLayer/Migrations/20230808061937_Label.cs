using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class Label : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Users_userId",
                table: "Notes");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Notes",
                newName: "userID");

            migrationBuilder.RenameIndex(
                name: "IX_Notes_userId",
                table: "Notes",
                newName: "IX_Notes_userID");

            migrationBuilder.CreateTable(
                name: "Labels",
                columns: table => new
                {
                    labelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    labelName = table.Column<string>(nullable: true),
                    userID = table.Column<int>(nullable: false),
                    NoteId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labels", x => x.labelId);
                    table.ForeignKey(
                        name: "FK_Labels_Notes_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Notes",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Labels_Users_userID",
                        column: x => x.userID,
                        principalTable: "Users",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Labels_NoteId",
                table: "Labels",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Labels_userID",
                table: "Labels",
                column: "userID");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Users_userID",
                table: "Notes",
                column: "userID",
                principalTable: "Users",
                principalColumn: "userID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Users_userID",
                table: "Notes");

            migrationBuilder.DropTable(
                name: "Labels");

            migrationBuilder.RenameColumn(
                name: "userID",
                table: "Notes",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_Notes_userID",
                table: "Notes",
                newName: "IX_Notes_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Users_userId",
                table: "Notes",
                column: "userId",
                principalTable: "Users",
                principalColumn: "userID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
