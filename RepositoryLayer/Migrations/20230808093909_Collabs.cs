using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class Collabs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Collabs",
                columns: table => new
                {
                    collabId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true),
                    NoteId = table.Column<int>(nullable: false),
                    userID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collabs", x => x.collabId);
                    table.ForeignKey(
                        name: "FK_Collabs_Collabs_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Collabs",
                        principalColumn: "collabId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Collabs_Collabs_userID",
                        column: x => x.userID,
                        principalTable: "Collabs",
                        principalColumn: "collabId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collabs_NoteId",
                table: "Collabs",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Collabs_userID",
                table: "Collabs",
                column: "userID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Collabs");
        }
    }
}
