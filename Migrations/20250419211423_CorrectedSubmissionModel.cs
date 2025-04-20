using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeasureAPI.Migrations
{
    /// <inheritdoc />
    public partial class CorrectedSubmissionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "MeasurementValues",
                newName: "Note");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Note",
                table: "MeasurementValues",
                newName: "Notes");
        }
    }
}
