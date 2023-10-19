using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace doanhuuthanh_web.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedIdentityUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("c5afba34-161c-4f72-8d63-1e6514c07924"), null, "Administrator role", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AppUser",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("1e103484-14e2-4083-bf1a-7e44301ed0d7"), 0, "8c0d96e9-e797-4f16-b8d6-a24fe0fa44e9", new DateTime(2002, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "thanh@gmail.com", true, "Thanh", "Doan Huu", false, null, "thanh@gmail.com", "admin", "AQAAAAIAAYagAAAAEG5M7T7WTIFIbW7GgeEegmzg0w7DLkh31I7fhqXl78vLrvKi54IcXGelohkvl66Y5A==", null, false, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("c5afba34-161c-4f72-8d63-1e6514c07924"), new Guid("1e103484-14e2-4083-bf1a-7e44301ed0d7") });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 8, 16, 23, 44, 53, 786, DateTimeKind.Local).AddTicks(9585));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRole",
                keyColumn: "Id",
                keyValue: new Guid("c5afba34-161c-4f72-8d63-1e6514c07924"));

            migrationBuilder.DeleteData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: new Guid("1e103484-14e2-4083-bf1a-7e44301ed0d7"));

            migrationBuilder.DeleteData(
                table: "AppUserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("c5afba34-161c-4f72-8d63-1e6514c07924"), new Guid("1e103484-14e2-4083-bf1a-7e44301ed0d7") });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 8, 16, 23, 15, 3, 233, DateTimeKind.Local).AddTicks(6682));
        }
    }
}
