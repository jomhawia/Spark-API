using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoneEvaluation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhoneEvaluations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PercentageOfScreen = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    PercentageOfBackScreen = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    PercentageOfBattery = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    PercentageOfCamera = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    PercentageOfOpen = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    PercentageOfOutScrren = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    PercentageOfBody = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    PercentageOfBiometrics = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneEvaluations", x => x.Id);
                    table.CheckConstraint("CHK_PercentageOfBackScreen", "[PercentageOfBackScreen] >= 0 AND [PercentageOfBackScreen] <= 100");
                    table.CheckConstraint("CHK_PercentageOfBattery", "[PercentageOfBattery] >= 0 AND [PercentageOfBattery] <= 100");
                    table.CheckConstraint("CHK_PercentageOfBiometrics", "[PercentageOfBiometrics] >= 0 AND [PercentageOfBiometrics] <= 100");
                    table.CheckConstraint("CHK_PercentageOfBody", "[PercentageOfBody] >= 0 AND [PercentageOfBody] <= 100");
                    table.CheckConstraint("CHK_PercentageOfCamera", "[PercentageOfCamera] >= 0 AND [PercentageOfCamera] <= 100");
                    table.CheckConstraint("CHK_PercentageOfOpen", "[PercentageOfOpen] >= 0 AND [PercentageOfOpen] <= 100");
                    table.CheckConstraint("CHK_PercentageOfOutScrren", "[PercentageOfOutScrren] >= 0 AND [PercentageOfOutScrren] <= 100");
                    table.CheckConstraint("CHK_PercentageOfScreen", "[PercentageOfScreen] >= 0 AND [PercentageOfScreen] <= 100");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhoneEvaluations_Name",
                table: "PhoneEvaluations",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhoneEvaluations");
        }
    }
}
