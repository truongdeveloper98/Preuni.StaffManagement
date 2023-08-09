using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreUni.StaffManagement.Database.Migrations
{
    /// <inheritdoc />
    public partial class updateBankAccountToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BSBNumber",
                table: "BankAccountDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "AccountNumber",
                table: "BankAccountDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "595017bc-1bbc-4bc1-b349-5b748eacee72", "AQAAAAIAAYagAAAAEFMSxHlQKASkWh2S/VyuRFJnXC2GTlOzmNLeLsvz5GyoL2jUooYiaoEJaCMX4odY1A==", "e13c7ae1-901a-4fb5-86d6-08827afa9c60" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a63e5513-ffbb-40ad-83e6-4f7b4a328a54", "AQAAAAIAAYagAAAAEGGdYBsjfeXH8dsuC9el6JvYyVEtA8tXFH47WBYO67lkD1R1RqLY/2z/otabDrLkCA==", "b66e097e-f9ef-423e-8641-b48fe84643af" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BSBNumber",
                table: "BankAccountDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "AccountNumber",
                table: "BankAccountDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
    }
}
