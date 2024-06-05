using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASM_PH35423.Migrations
{
    /// <inheritdoc />
    public partial class ao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mode",
                table: "ExamHistories");

            migrationBuilder.RenameColumn(
                name: "NumbersTrue",
                table: "ExamHistoryDetails",
                newName: "NumbersChose");

            migrationBuilder.AddColumn<string>(
                name: "Img",
                table: "ExamHistoryDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Info",
                table: "ExamHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Img",
                table: "ExamHistoryDetails");

            migrationBuilder.DropColumn(
                name: "Info",
                table: "ExamHistories");

            migrationBuilder.RenameColumn(
                name: "NumbersChose",
                table: "ExamHistoryDetails",
                newName: "NumbersTrue");

            migrationBuilder.AddColumn<int>(
                name: "Mode",
                table: "ExamHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
