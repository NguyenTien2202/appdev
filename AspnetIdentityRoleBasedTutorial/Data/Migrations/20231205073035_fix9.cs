﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspnetIdentityRoleBasedTutorial.Data.Migrations
{
    /// <inheritdoc />
    public partial class fix9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ShoppingCartItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ShoppingCartItems");
        }
    }
}
