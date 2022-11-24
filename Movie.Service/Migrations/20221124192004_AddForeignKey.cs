using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Movies.Service.Migrations
{
    public partial class AddForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Drinks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Drinks_UserId",
                table: "Drinks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drinks_Users_UserId",
                table: "Drinks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drinks_Users_UserId",
                table: "Drinks");

            migrationBuilder.DropIndex(
                name: "IX_Drinks_UserId",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Drinks");
        }
    }
}
