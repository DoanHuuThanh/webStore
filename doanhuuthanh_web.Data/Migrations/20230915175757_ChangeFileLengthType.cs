using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace doanhuuthanh_web.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFileLengthType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "FileSize",
                table: "ProductImage",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: new Guid("1e103484-14e2-4083-bf1a-7e44301ed0d7"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fbcd27ab-5cb0-4a65-a6ea-5f4374981a6f", "AQAAAAIAAYagAAAAEFcuMA038u9t+CRUqiA8Bw47LW0kFF6EkappyQ+/gkIW793abp9HN4OWSk04zcVA+Q==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 9, 16, 0, 57, 57, 310, DateTimeKind.Local).AddTicks(4074));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FileSize",
                table: "ProductImage",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: new Guid("1e103484-14e2-4083-bf1a-7e44301ed0d7"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "411dad0d-277c-4333-976c-3ef47e865cfe", "AQAAAAIAAYagAAAAEJef0fpZj0jRatjLM0u9UwTnqga71NZz1mGnc23YN+lpQtTv+X4HAq7rUeekvrS+Dw==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 9, 14, 23, 36, 4, 629, DateTimeKind.Local).AddTicks(997));
        }
    }
}
