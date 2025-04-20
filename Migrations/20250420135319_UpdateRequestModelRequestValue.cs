using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeasureAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRequestModelRequestValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MeasurementValues_MeasurementRequestId",
                table: "MeasurementValues");

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementValues_MeasurementRequestId",
                table: "MeasurementValues",
                column: "MeasurementRequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MeasurementValues_MeasurementRequestId",
                table: "MeasurementValues");

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementValues_MeasurementRequestId",
                table: "MeasurementValues",
                column: "MeasurementRequestId",
                unique: true);
        }
    }
}
