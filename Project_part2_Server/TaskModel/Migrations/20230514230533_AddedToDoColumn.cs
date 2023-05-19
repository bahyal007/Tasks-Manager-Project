using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskModel.Migrations
{
    /// <inheritdoc />
    public partial class AddedToDoColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DaysLeft",
                table: "ToDoItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaysLeft",
                table: "ToDoItems");
        }
    }
}
