using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BKSHLF_API.Migrations
{
    public partial class AddEditionsAndPublishers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Editions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ISBN = table.Column<string>(type: "varchar(10)", nullable: false),
                    ISBN13 = table.Column<string>(type: "varchar(13)", nullable: true),
                    NumberOfPages = table.Column<int>(nullable: false),
                    Language = table.Column<string>(nullable: true),
                    Format = table.Column<int>(nullable: false),
                    BookId = table.Column<Guid>(nullable: false),
                    PublisherId = table.Column<int>(nullable: true),
                    PublishedAt = table.Column<DateTime>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Editions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Editions_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Editions_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Editions_BookId",
                table: "Editions",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Editions_ISBN",
                table: "Editions",
                column: "ISBN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Editions_ISBN13",
                table: "Editions",
                column: "ISBN13",
                unique: true,
                filter: "[ISBN13] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Editions_PublisherId",
                table: "Editions",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_Name",
                table: "Publishers",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Editions");

            migrationBuilder.DropTable(
                name: "Publishers");
        }
    }
}
