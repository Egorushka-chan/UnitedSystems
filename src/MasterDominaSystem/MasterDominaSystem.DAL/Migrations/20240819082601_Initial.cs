using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MasterDominaSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportCloths",
                columns: table => new
                {
                    GeneralID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClothID = table.Column<int>(type: "integer", nullable: false),
                    ClothName = table.Column<string>(type: "text", nullable: false),
                    ClothDescription = table.Column<string>(type: "text", nullable: true),
                    ClothRating = table.Column<int>(type: "integer", nullable: false),
                    ClothSize = table.Column<string>(type: "text", nullable: false),
                    MaterialID = table.Column<int>(type: "integer", nullable: true),
                    MaterialName = table.Column<string>(type: "text", nullable: true),
                    MaterialDescription = table.Column<string>(type: "text", nullable: true),
                    PhotoID = table.Column<int>(type: "integer", nullable: true),
                    PhotoName = table.Column<string>(type: "text", nullable: true),
                    PhotoHashCode = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportCloths", x => x.GeneralID);
                });

            migrationBuilder.CreateTable(
                name: "ReportPersons",
                columns: table => new
                {
                    GeneralID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PersonID = table.Column<int>(type: "integer", nullable: false),
                    PersonName = table.Column<string>(type: "text", nullable: false),
                    PersonType = table.Column<string>(type: "text", nullable: false),
                    PhysiqueID = table.Column<int>(type: "integer", nullable: true),
                    PhysiqueWeight = table.Column<int>(type: "integer", nullable: true),
                    PhysiqueGrowth = table.Column<int>(type: "integer", nullable: true),
                    PhysiqueForce = table.Column<int>(type: "integer", nullable: true),
                    PhysiqueDescription = table.Column<string>(type: "text", nullable: true),
                    SetID = table.Column<int>(type: "integer", nullable: true),
                    SetName = table.Column<string>(type: "text", nullable: true),
                    SetDescription = table.Column<string>(type: "text", nullable: true),
                    SeasonID = table.Column<int>(type: "integer", nullable: true),
                    SeasonName = table.Column<string>(type: "text", nullable: true),
                    ClothID = table.Column<int>(type: "integer", nullable: true),
                    ClothName = table.Column<string>(type: "text", nullable: true),
                    ClothDescription = table.Column<string>(type: "text", nullable: true),
                    ClothRating = table.Column<int>(type: "integer", nullable: true),
                    ClothSize = table.Column<string>(type: "text", nullable: true),
                    PhotoID = table.Column<int>(type: "integer", nullable: true),
                    PhotoName = table.Column<string>(type: "text", nullable: true),
                    PhotoHashCode = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportPersons", x => x.GeneralID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportCloths_ClothID_MaterialID_PhotoID",
                table: "ReportCloths",
                columns: ["ClothID", "MaterialID", "PhotoID"]);

            migrationBuilder.CreateIndex(
                name: "IX_ReportPersons_PersonID_PhysiqueID_SetID_SeasonID_ClothID_Ph~",
                table: "ReportPersons",
                columns: ["PersonID", "PhysiqueID", "SetID", "SeasonID", "ClothID", "PhotoID"]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportCloths");

            migrationBuilder.DropTable(
                name: "ReportPersons");
        }
    }
}
