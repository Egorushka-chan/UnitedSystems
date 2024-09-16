using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterDominaSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Typos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClothHasMaterials_Material_MaterialID",
                table: "ClothHasMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_Physique_Persons_PersonID",
                table: "Physique");

            migrationBuilder.DropForeignKey(
                name: "FK_Sets_Physique_PhysiqueID",
                table: "Sets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Physique",
                table: "Physique");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Material",
                table: "Material");

            migrationBuilder.RenameTable(
                name: "Physique",
                newName: "Physiques");

            migrationBuilder.RenameTable(
                name: "Material",
                newName: "Materials");

            migrationBuilder.RenameIndex(
                name: "IX_Physique_PersonID",
                table: "Physiques",
                newName: "IX_Physiques_PersonID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Physiques",
                table: "Physiques",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Materials",
                table: "Materials",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ClothHasMaterials_Materials_MaterialID",
                table: "ClothHasMaterials",
                column: "MaterialID",
                principalTable: "Materials",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Physiques_Persons_PersonID",
                table: "Physiques",
                column: "PersonID",
                principalTable: "Persons",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_Physiques_PhysiqueID",
                table: "Sets",
                column: "PhysiqueID",
                principalTable: "Physiques",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClothHasMaterials_Materials_MaterialID",
                table: "ClothHasMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_Physiques_Persons_PersonID",
                table: "Physiques");

            migrationBuilder.DropForeignKey(
                name: "FK_Sets_Physiques_PhysiqueID",
                table: "Sets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Physiques",
                table: "Physiques");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Materials",
                table: "Materials");

            migrationBuilder.RenameTable(
                name: "Physiques",
                newName: "Physique");

            migrationBuilder.RenameTable(
                name: "Materials",
                newName: "Material");

            migrationBuilder.RenameIndex(
                name: "IX_Physiques_PersonID",
                table: "Physique",
                newName: "IX_Physique_PersonID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Physique",
                table: "Physique",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Material",
                table: "Material",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ClothHasMaterials_Material_MaterialID",
                table: "ClothHasMaterials",
                column: "MaterialID",
                principalTable: "Material",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Physique_Persons_PersonID",
                table: "Physique",
                column: "PersonID",
                principalTable: "Persons",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_Physique_PhysiqueID",
                table: "Sets",
                column: "PhysiqueID",
                principalTable: "Physique",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
