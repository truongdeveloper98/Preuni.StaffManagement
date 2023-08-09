using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreUni.StaffManagement.Database.Migrations
{
    /// <inheritdoc />
    public partial class TFNDeclareChangeToDateTimeOffset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "SignDate",
                table: "TFNDeclares",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "57b675d9-be19-45d6-a0b9-50eed84d93c7", "AQAAAAIAAYagAAAAEJpk6qLBmfTsSlISTT/UkofBjmLXZS9KaXqqfVVUZZyp7AW+TxJhXCbQ2o7wmslssg==", "0c8cde23-e0dc-44b7-aecd-3c51a407924a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3f423417-5c64-4b9a-8001-a9b4b35f8d09", "AQAAAAIAAYagAAAAEDoKxOu48moDcfPSoblC6xA63VutSLoW1N2rapnjvX8+CCjqk8l80Pued5wpL0FmZQ==", "c388bd4f-ea03-4519-8eea-0ea36f6ac146" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SignDate",
                table: "TFNDeclares",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f3e82a11-19fb-448b-b97a-58fab8aea2e4", "AQAAAAIAAYagAAAAED9DB7jmwJ0RCfOs85wAaeT5r4E8AfLEGY3Cm8NAH7ggjfxr4ZbbNSpyV0NqzEMBeg==", "e5de18c3-34c4-4035-a4fe-da4ee22d771a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eb1021d4-f1c2-4ca9-a757-a5ee9f519180", "AQAAAAIAAYagAAAAECwuR+6QDZwfcDiR7v2cZ1YBcCvzrKNlOeNSTjfwe7FBnQNbrIOvGxkdydbDihWNgA==", "be6e3e63-204d-4dfc-b9a2-231bc4e1303e" });
        }
    }
}
