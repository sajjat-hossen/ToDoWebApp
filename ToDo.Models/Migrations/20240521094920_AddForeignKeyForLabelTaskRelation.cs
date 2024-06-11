using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDo.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyForLabelTaskRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LabelId",
                table: "ToDoTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ToDoTasks_LabelId",
                table: "ToDoTasks",
                column: "LabelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoTasks_Labels_LabelId",
                table: "ToDoTasks",
                column: "LabelId",
                principalTable: "Labels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoTasks_Labels_LabelId",
                table: "ToDoTasks");

            migrationBuilder.DropIndex(
                name: "IX_ToDoTasks_LabelId",
                table: "ToDoTasks");

            migrationBuilder.DropColumn(
                name: "LabelId",
                table: "ToDoTasks");
        }
    }
}
