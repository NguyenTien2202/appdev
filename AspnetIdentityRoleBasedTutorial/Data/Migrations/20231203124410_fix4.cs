using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspnetIdentityRoleBasedTutorial.Data.Migrations
{
    /// <inheritdoc />
    public partial class fix4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Detail",
                table: "Jewelries");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Jewelries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jewelries_CategoryId",
                table: "Jewelries",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jewelries_Category_CategoryId",
                table: "Jewelries",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jewelries_Category_CategoryId",
                table: "Jewelries");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Jewelries_CategoryId",
                table: "Jewelries");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Jewelries");

            migrationBuilder.AddColumn<string>(
                name: "Detail",
                table: "Jewelries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
