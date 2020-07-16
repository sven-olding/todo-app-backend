using Microsoft.EntityFrameworkCore.Migrations;

namespace todo_app_backend.Migrations
{
    public partial class AddSecretProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Secret",
                table: "ToDoItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Secret",
                table: "ToDoItems");
        }
    }
}
