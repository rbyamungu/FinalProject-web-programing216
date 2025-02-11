using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HalfAndHalf.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "company",
                columns: table => new
                {
                    company_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    company_name = table.Column<string>(type: "text", nullable: false),
                    org_type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company", x => x.company_id);
                });

            migrationBuilder.CreateTable(
                name: "railroad",
                columns: table => new
                {
                    railroad_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    railroad_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_railroad", x => x.railroad_id);
                });

            migrationBuilder.CreateTable(
                name: "incident_train",
                columns: table => new
                {
                    train_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name_number = table.Column<string>(type: "text", nullable: false),
                    train_type = table.Column<string>(type: "text", nullable: false),
                    railroad_id = table.Column<int>(type: "integer", nullable: true),
                    railroad_id1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_incident_train", x => x.train_id);
                    table.ForeignKey(
                        name: "incident_train_railroad_id_fkey",
                        column: x => x.railroad_id,
                        principalTable: "railroad",
                        principalColumn: "railroad_id");
                });

            migrationBuilder.CreateTable(
                name: "incident",
                columns: table => new
                {
                    seqnos = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    company_id = table.Column<int>(type: "integer", nullable: true),
                    railroad_id = table.Column<int>(type: "integer", nullable: true),
                    incident_train_id = table.Column<int>(type: "integer", nullable: true),
                    date_time_received = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_time_complete = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    call_type = table.Column<string>(type: "text", nullable: false),
                    responsible_city = table.Column<string>(type: "text", nullable: true),
                    responsible_state = table.Column<string>(type: "text", nullable: true),
                    responsible_zip = table.Column<string>(type: "text", nullable: true),
                    description_of_incident = table.Column<string>(type: "text", nullable: true),
                    type_of_incident = table.Column<string>(type: "text", nullable: false),
                    incident_cause = table.Column<string>(type: "text", nullable: false),
                    injury_count = table.Column<int>(type: "integer", nullable: true),
                    hospitalization_count = table.Column<int>(type: "integer", nullable: true),
                    fatality_count = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_incident", x => x.seqnos);
                    table.ForeignKey(
                        name: "incident_company_id_fkey",
                        column: x => x.company_id,
                        principalTable: "company",
                        principalColumn: "company_id");
                    table.ForeignKey(
                        name: "incident_incident_train_id_fkey",
                        column: x => x.incident_train_id,
                        principalTable: "incident_train",
                        principalColumn: "train_id");
                    table.ForeignKey(
                        name: "incident_railroad_id_fkey",
                        column: x => x.railroad_id,
                        principalTable: "railroad",
                        principalColumn: "railroad_id");
                });

            migrationBuilder.CreateTable(
                name: "incident_train_car",
                columns: table => new
                {
                    TrainCarId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CarNumber = table.Column<string>(type: "text", nullable: false),
                    CarContent = table.Column<string>(type: "text", nullable: false),
                    PositionInTrain = table.Column<int>(type: "integer", nullable: true),
                    CarType = table.Column<string>(type: "text", nullable: false),
                    IncidentTrainId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_incident_train_car", x => x.TrainCarId);
                    table.ForeignKey(
                        name: "FK_incident_train_car_incident_train_IncidentTrainId",
                        column: x => x.IncidentTrainId,
                        principalTable: "incident_train",
                        principalColumn: "train_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_incident_company_id",
                table: "incident",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_incident_incident_train_id",
                table: "incident",
                column: "incident_train_id");

            migrationBuilder.CreateIndex(
                name: "IX_incident_railroad_id",
                table: "incident",
                column: "railroad_id");

            migrationBuilder.CreateIndex(
                name: "IX_incident_train_railroad_id",
                table: "incident_train",
                column: "railroad_id");

            migrationBuilder.CreateIndex(
                name: "IX_incident_train_car_IncidentTrainId",
                table: "incident_train_car",
                column: "IncidentTrainId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "incident");

            migrationBuilder.DropTable(
                name: "incident_train_car");

            migrationBuilder.DropTable(
                name: "company");

            migrationBuilder.DropTable(
                name: "incident_train");

            migrationBuilder.DropTable(
                name: "railroad");
        }
    }
}
