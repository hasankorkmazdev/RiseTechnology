using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RiseTechnology.Contact.API.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "RiseContact");

            migrationBuilder.CreateTable(
                name: "Persons",
                schema: "RiseContact",
                columns: table => new
                {
                    UUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.UUID);
                });

            migrationBuilder.CreateTable(
                name: "ContactInformations",
                schema: "RiseContact",
                columns: table => new
                {
                    UUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactType = table.Column<int>(type: "int", nullable: false),
                    ContactContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonUUID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInformations", x => x.UUID);
                    table.ForeignKey(
                        name: "FK_ContactInformations_Persons_PersonUUID",
                        column: x => x.PersonUUID,
                        principalSchema: "RiseContact",
                        principalTable: "Persons",
                        principalColumn: "UUID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactInformations_PersonUUID",
                schema: "RiseContact",
                table: "ContactInformations",
                column: "PersonUUID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactInformations",
                schema: "RiseContact");

            migrationBuilder.DropTable(
                name: "Persons",
                schema: "RiseContact");
        }
    }
}
