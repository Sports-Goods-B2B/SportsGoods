using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsGoods.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBrandConfigurationForMediaProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_Media_PictureId",
                table: "Brands");

            migrationBuilder.DropIndex(
                name: "IX_Brands_PictureId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "PictureId",
                table: "Brands");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Blob",
                table: "Media",
                type: "varbinary(MAX)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Brands_Id",
                table: "Media",
                column: "Id",
                principalTable: "Brands",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_Brands_Id",
                table: "Media");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Blob",
                table: "Media",
                type: "varbinary(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(MAX)");

            migrationBuilder.AddColumn<Guid>(
                name: "PictureId",
                table: "Brands",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brands_PictureId",
                table: "Brands",
                column: "PictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_Media_PictureId",
                table: "Brands",
                column: "PictureId",
                principalTable: "Media",
                principalColumn: "Id");
        }
    }
}
