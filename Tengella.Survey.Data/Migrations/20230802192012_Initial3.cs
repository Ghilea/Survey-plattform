using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tengella.Survey.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Templates_StatisticsQuestions_StatisticQuestionId",
                table: "Templates");

            migrationBuilder.DropIndex(
                name: "IX_Templates_StatisticQuestionId",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "StatisticQuestionId",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "TemplateId",
                table: "StatisticsQuestions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatisticQuestionId",
                table: "Templates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TemplateId",
                table: "StatisticsQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Templates_StatisticQuestionId",
                table: "Templates",
                column: "StatisticQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Templates_StatisticsQuestions_StatisticQuestionId",
                table: "Templates",
                column: "StatisticQuestionId",
                principalTable: "StatisticsQuestions",
                principalColumn: "Id");
        }
    }
}
