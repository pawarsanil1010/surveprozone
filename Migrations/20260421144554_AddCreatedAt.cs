using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveProzone.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminUsers_AdminUsers_AdminUserId",
                table: "AdminUsers");

            migrationBuilder.DropIndex(
                name: "IX_AdminUsers_AdminUserId",
                table: "AdminUsers");

            migrationBuilder.DropColumn(
                name: "AdminUserId",
                table: "AdminUsers");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ContactForms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ContactForms");

            migrationBuilder.AddColumn<int>(
                name: "AdminUserId",
                table: "AdminUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdminUsers_AdminUserId",
                table: "AdminUsers",
                column: "AdminUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminUsers_AdminUsers_AdminUserId",
                table: "AdminUsers",
                column: "AdminUserId",
                principalTable: "AdminUsers",
                principalColumn: "Id");
        }
    }
}
