using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiSqlServer.Migrations
{
    /// <inheritdoc />
    public partial class newfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Venta",
                table: "Productos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Venta",
                table: "Productos");
        }
    }
}
