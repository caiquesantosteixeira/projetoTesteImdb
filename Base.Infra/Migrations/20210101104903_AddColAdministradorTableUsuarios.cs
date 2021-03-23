using Microsoft.EntityFrameworkCore.Migrations;

namespace Base.Repository.Migrations
{
    public partial class AddColAdministradorTableUsuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "administrador",
                table: "usuarios",
                nullable: true,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "administrador",
                table: "usuarios");
        }
    }
}
