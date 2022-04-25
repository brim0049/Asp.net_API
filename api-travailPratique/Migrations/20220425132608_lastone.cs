using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_travailPratique.Migrations
{
    public partial class lastone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Factures_Clients_ClientId",
                table: "Factures");

            migrationBuilder.DropForeignKey(
                name: "FK_Factures_Vendeurs_VendeurId",
                table: "Factures");

            migrationBuilder.AlterColumn<int>(
                name: "VendeurId",
                table: "Factures",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Factures",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Factures_Clients_ClientId",
                table: "Factures",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Factures_Vendeurs_VendeurId",
                table: "Factures",
                column: "VendeurId",
                principalTable: "Vendeurs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Factures_Clients_ClientId",
                table: "Factures");

            migrationBuilder.DropForeignKey(
                name: "FK_Factures_Vendeurs_VendeurId",
                table: "Factures");

            migrationBuilder.AlterColumn<int>(
                name: "VendeurId",
                table: "Factures",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Factures",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Factures_Clients_ClientId",
                table: "Factures",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Factures_Vendeurs_VendeurId",
                table: "Factures",
                column: "VendeurId",
                principalTable: "Vendeurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
