using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CedroRestauranteWS.Migrations
{
    public partial class CascadeDeleting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pratos_Restaurantes_RestauranteId",
                table: "Pratos");

            migrationBuilder.AddForeignKey(
                name: "FK_Pratos_Restaurantes_RestauranteId",
                table: "Pratos",
                column: "RestauranteId",
                principalTable: "Restaurantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pratos_Restaurantes_RestauranteId",
                table: "Pratos");

            migrationBuilder.AddForeignKey(
                name: "FK_Pratos_Restaurantes_RestauranteId",
                table: "Pratos",
                column: "RestauranteId",
                principalTable: "Restaurantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
