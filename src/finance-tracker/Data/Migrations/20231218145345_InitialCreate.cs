using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace finance_tracker.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryTrees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Data = table.Column<string>(type: "TEXT", nullable: false),
                    ParentId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTrees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryTrees_CategoryTrees_ParentId",
                        column: x => x.ParentId,
                        principalTable: "CategoryTrees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TreeNodestringId = table.Column<int>(name: "TreeNode<string>Id", type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_CategoryTrees_TreeNode<string>Id",
                        column: x => x.TreeNodestringId,
                        principalTable: "CategoryTrees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tree<string>",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RootId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tree<string>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tree<string>_CategoryTrees_RootId",
                        column: x => x.RootId,
                        principalTable: "CategoryTrees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    CategoryTreeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Tree<string>_CategoryTreeId",
                        column: x => x.CategoryTreeId,
                        principalTable: "Tree<string>",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTrees_ParentId",
                table: "CategoryTrees",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_TreeNode<string>Id",
                table: "Transaction",
                column: "TreeNode<string>Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tree<string>_RootId",
                table: "Tree<string>",
                column: "RootId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CategoryTreeId",
                table: "Users",
                column: "CategoryTreeId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Tree<string>");

            migrationBuilder.DropTable(
                name: "CategoryTrees");
        }
    }
}
