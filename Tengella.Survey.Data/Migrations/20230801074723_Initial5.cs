using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tengella.Survey.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLastTimeSender",
                table: "Distribution",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Distribution",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsLastTimeSender",
                value: false);

            migrationBuilder.UpdateData(
                table: "Distribution",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsLastTimeSender",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLastTimeSender",
                table: "Distribution");
        }
    }
}
