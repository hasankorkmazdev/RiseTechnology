using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace RiseTechnology.Contact.API.Migrations
{
    public partial class ContactInformationPersonUUIDFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactInformations_Persons_PersonUUID",
                schema: "RiseContact",
                table: "ContactInformations");

            migrationBuilder.AlterColumn<Guid>(
                name: "PersonUUID",
                schema: "RiseContact",
                table: "ContactInformations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactInformations_Persons_PersonUUID",
                schema: "RiseContact",
                table: "ContactInformations",
                column: "PersonUUID",
                principalSchema: "RiseContact",
                principalTable: "Persons",
                principalColumn: "UUID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactInformations_Persons_PersonUUID",
                schema: "RiseContact",
                table: "ContactInformations");

            migrationBuilder.AlterColumn<Guid>(
                name: "PersonUUID",
                schema: "RiseContact",
                table: "ContactInformations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactInformations_Persons_PersonUUID",
                schema: "RiseContact",
                table: "ContactInformations",
                column: "PersonUUID",
                principalSchema: "RiseContact",
                principalTable: "Persons",
                principalColumn: "UUID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
