using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce.Migrations
{
    /// <inheritdoc />
    public partial class addColumnEmailConfirm2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "email_cofirmed",
                table: "users",
                newName: "email_confirmed");

            migrationBuilder.AddColumn<int>(
                name: "email_otp_attempts",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "email_otp_code",
                table: "users",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "email_otp_expires_at",
                table: "users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "email_otp_locked_until",
                table: "users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "email_otp_sent_at",
                table: "users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_users_email",
                table: "users");

            migrationBuilder.DropColumn(
                name: "email_otp_attempts",
                table: "users");

            migrationBuilder.DropColumn(
                name: "email_otp_code",
                table: "users");

            migrationBuilder.DropColumn(
                name: "email_otp_expires_at",
                table: "users");

            migrationBuilder.DropColumn(
                name: "email_otp_locked_until",
                table: "users");

            migrationBuilder.DropColumn(
                name: "email_otp_sent_at",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "email_confirmed",
                table: "users",
                newName: "email_cofirmed");
        }
    }
}
