using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce.Migrations
{
    /// <inheritdoc />
    public partial class addMoreColumToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "reset_otp_attempts",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "reset_otp_code",
                table: "users",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "reset_otp_expires_at",
                table: "users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "reset_otp_locked_until",
                table: "users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "reset_otp_sent_at",
                table: "users",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "reset_otp_attempts",
                table: "users");

            migrationBuilder.DropColumn(
                name: "reset_otp_code",
                table: "users");

            migrationBuilder.DropColumn(
                name: "reset_otp_expires_at",
                table: "users");

            migrationBuilder.DropColumn(
                name: "reset_otp_locked_until",
                table: "users");

            migrationBuilder.DropColumn(
                name: "reset_otp_sent_at",
                table: "users");
        }
    }
}
