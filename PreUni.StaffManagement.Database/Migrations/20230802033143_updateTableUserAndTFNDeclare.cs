using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreUni.StaffManagement.Database.Migrations
{
    /// <inheritdoc />
    public partial class updateTableUserAndTFNDeclare : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "OtherName",
                table: "TFNDeclares",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "FaxNumberOption",
                table: "TFNDeclares",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1762e53c-95e7-4ad3-b6fc-02a977049c4b", "AQAAAAIAAYagAAAAEPkOxKAsEkr0BcsjT4RNMoBPbieps+igJNQ87SZmV/5O+IZ3Gd44/7OiRHCRpTv3bg==", "fb0296f8-2c30-4a09-85bb-7790e7680069" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "41b915a9-06a9-4feb-96af-0cddbd7ac20c", "AQAAAAIAAYagAAAAENgKxZkPd3gOAosjxioioE1rJR4VBR8/vM2U2X8IodQF89Y3sltFsj8QmsNTV5lpAA==", "60dbdc1d-0eab-4302-b20f-c454ee7d11b5" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FaxNumberOption",
                table: "TFNDeclares");

            migrationBuilder.AlterColumn<string>(
                name: "OtherName",
                table: "TFNDeclares",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "Password", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4c4e8146-8020-4121-907b-8f833de5bf6f", "admin", "AQAAAAIAAYagAAAAEEeeawysTVu2XIWNTXjkZjHEYRjqJvxAYvoAiy5gEfbop7k0OdM/YDJu0TWzB7vdog==", "201980ce-7ec2-4ee1-8ae9-d86bc076cee2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "Password", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1f43112e-32ed-4d57-9d0b-fff99ae1630d", "applicant", "AQAAAAIAAYagAAAAEEkSsvo4A7ZJujhgpcEPCYbzQhfXzJ7Z96KmSobXieVfeFJSgBInEPmKXzzeThr+nw==", "daa6d2e5-6507-4616-b140-5c020ea80bb6" });
        }
    }
}
