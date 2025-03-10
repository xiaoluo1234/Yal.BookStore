using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yal.BookStore.Migrations
{
    /// <inheritdoc />
    public partial class Add_Book_Author : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "编码"),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false, comment: "名字"),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true, comment: "描述")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.Id);
                },
                comment: "作者");

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false, comment: "书名"),
                    AuthorCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "作者编码"),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true, comment: "描述")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                },
                comment: "图书");

            migrationBuilder.CreateIndex(
                name: "IX_Author_Code",
                table: "Author",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Book_AuthorCode",
                table: "Book",
                column: "AuthorCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "Book");
        }
    }
}
