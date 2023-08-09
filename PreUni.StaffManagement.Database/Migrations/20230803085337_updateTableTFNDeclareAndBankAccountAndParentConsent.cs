using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreUni.StaffManagement.Database.Migrations
{
    /// <inheritdoc />
    public partial class updateTableTFNDeclareAndBankAccountAndParentConsent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FaxNumberOption",
                table: "TFNDeclares",
                newName: "TaxNumberOption");

            migrationBuilder.RenameColumn(
                name: "FaxNumber",
                table: "TFNDeclares",
                newName: "TaxNumber");

            migrationBuilder.AddColumn<DateTime>(
                name: "SignDate",
                table: "TFNDeclares",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Signature",
                table: "TFNDeclares",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApplicantName",
                table: "ParentConsents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RelationshipApplicantFirst",
                table: "ParentConsents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RelationshipApplicantSecond",
                table: "ParentConsents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "SignDate",
                table: "ParentConsents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Signature",
                table: "ParentConsents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameApplicant",
                table: "BankAccountDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "SignDate",
                table: "BankAccountDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Signature",
                table: "BankAccountDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SignDate",
                table: "TFNDeclares");

            migrationBuilder.DropColumn(
                name: "Signature",
                table: "TFNDeclares");

            migrationBuilder.DropColumn(
                name: "ApplicantName",
                table: "ParentConsents");

            migrationBuilder.DropColumn(
                name: "RelationshipApplicantFirst",
                table: "ParentConsents");

            migrationBuilder.DropColumn(
                name: "RelationshipApplicantSecond",
                table: "ParentConsents");

            migrationBuilder.DropColumn(
                name: "SignDate",
                table: "ParentConsents");

            migrationBuilder.DropColumn(
                name: "Signature",
                table: "ParentConsents");

            migrationBuilder.DropColumn(
                name: "NameApplicant",
                table: "BankAccountDetails");

            migrationBuilder.DropColumn(
                name: "SignDate",
                table: "BankAccountDetails");

            migrationBuilder.DropColumn(
                name: "Signature",
                table: "BankAccountDetails");

            migrationBuilder.RenameColumn(
                name: "TaxNumberOption",
                table: "TFNDeclares",
                newName: "FaxNumberOption");

            migrationBuilder.RenameColumn(
                name: "TaxNumber",
                table: "TFNDeclares",
                newName: "FaxNumber");

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
    }
}
