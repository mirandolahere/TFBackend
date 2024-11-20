using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiSqlServer.Migrations
{
    /// <inheritdoc />
    public partial class newtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Disponibilidad",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "fechaExpiracion",
                table: "Productos",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Disponibilidad",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "fechaExpiracion",
                table: "Productos");
        }
    }
}
