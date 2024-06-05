using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASM_PH35423.Migrations
{
    /// <inheritdoc />
    public partial class adđf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Question",
                table: "ExamHistoryDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Question",
                table: "ExamHistoryDetails");
        }
    }
}
