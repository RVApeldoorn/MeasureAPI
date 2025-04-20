using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeasureAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddMeasurementSessions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeasurementRequests_Patients_PatientId",
                table: "MeasurementRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_MeasurementRequests_Users_RequestedByUserId",
                table: "MeasurementRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_MeasurementValues_MeasurementTypes_MeasurementTypeId",
                table: "MeasurementValues");

            migrationBuilder.DropForeignKey(
                name: "FK_MeasurementValues_Patients_PatientId",
                table: "MeasurementValues");

            migrationBuilder.DropIndex(
                name: "IX_MeasurementValues_MeasurementTypeId",
                table: "MeasurementValues");

            migrationBuilder.DropIndex(
                name: "IX_MeasurementValues_PatientId",
                table: "MeasurementValues");

            migrationBuilder.DropIndex(
                name: "IX_MeasurementRequests_PatientId",
                table: "MeasurementRequests");

            migrationBuilder.DropColumn(
                name: "MeasurementTypeId",
                table: "MeasurementValues");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "MeasurementValues");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "MeasurementRequests");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "MeasurementRequests");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "MeasurementRequests");

            migrationBuilder.RenameColumn(
                name: "RequestedByUserId",
                table: "MeasurementRequests",
                newName: "MeasurementSessionId");

            migrationBuilder.RenameIndex(
                name: "IX_MeasurementRequests_RequestedByUserId",
                table: "MeasurementRequests",
                newName: "IX_MeasurementRequests_MeasurementSessionId");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "MeasurementValues",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "MeasurementValues",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MeasurementSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PatientId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsCompleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DueDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeasurementSessions_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeasurementSessions_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementValues_UserId",
                table: "MeasurementValues",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementSessions_CreatedByUserId",
                table: "MeasurementSessions",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementSessions_PatientId",
                table: "MeasurementSessions",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeasurementRequests_MeasurementSessions_MeasurementSessionId",
                table: "MeasurementRequests",
                column: "MeasurementSessionId",
                principalTable: "MeasurementSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeasurementValues_Users_UserId",
                table: "MeasurementValues",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeasurementRequests_MeasurementSessions_MeasurementSessionId",
                table: "MeasurementRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_MeasurementValues_Users_UserId",
                table: "MeasurementValues");

            migrationBuilder.DropTable(
                name: "MeasurementSessions");

            migrationBuilder.DropIndex(
                name: "IX_MeasurementValues_UserId",
                table: "MeasurementValues");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "MeasurementValues");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MeasurementValues");

            migrationBuilder.RenameColumn(
                name: "MeasurementSessionId",
                table: "MeasurementRequests",
                newName: "RequestedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_MeasurementRequests_MeasurementSessionId",
                table: "MeasurementRequests",
                newName: "IX_MeasurementRequests_RequestedByUserId");

            migrationBuilder.AddColumn<int>(
                name: "MeasurementTypeId",
                table: "MeasurementValues",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "MeasurementValues",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "MeasurementRequests",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "MeasurementRequests",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "MeasurementRequests",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementValues_MeasurementTypeId",
                table: "MeasurementValues",
                column: "MeasurementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementValues_PatientId",
                table: "MeasurementValues",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementRequests_PatientId",
                table: "MeasurementRequests",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeasurementRequests_Patients_PatientId",
                table: "MeasurementRequests",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeasurementRequests_Users_RequestedByUserId",
                table: "MeasurementRequests",
                column: "RequestedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeasurementValues_MeasurementTypes_MeasurementTypeId",
                table: "MeasurementValues",
                column: "MeasurementTypeId",
                principalTable: "MeasurementTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeasurementValues_Patients_PatientId",
                table: "MeasurementValues",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
