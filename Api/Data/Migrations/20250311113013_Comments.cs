using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class Comments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Purchase",
                table: "Stocks",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "LastDiv",
                table: "Stocks",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AddColumn<int>(
                name: "StockId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "CreatedOn", "StockId", "Title" },
                values: new object[] { 1, "Tesla Comment", new DateTime(2025, 3, 11, 12, 30, 13, 413, DateTimeKind.Local).AddTicks(596), 1, "TSLA Comment" });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_StockId",
                table: "Comments",
                column: "StockId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Stocks_StockId",
                table: "Comments",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Stocks_StockId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_StockId",
                table: "Comments");

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "StockId",
                table: "Comments");

            migrationBuilder.AlterColumn<decimal>(
                name: "Purchase",
                table: "Stocks",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "LastDiv",
                table: "Stocks",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
