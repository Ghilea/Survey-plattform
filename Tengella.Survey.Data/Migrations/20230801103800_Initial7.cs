using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tengella.Survey.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLastTimeSender",
                table: "Distribution");

            migrationBuilder.DropColumn(
                name: "TemplateId",
                table: "Distribution");

            migrationBuilder.AddColumn<int>(
                name: "TemplateSenderListId",
                table: "Distribution",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TemplateSenderList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TemplatesId = table.Column<int>(type: "int", nullable: false),
                    DistributionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateSenderList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TemplateTemplateSenderList",
                columns: table => new
                {
                    SendersId = table.Column<int>(type: "int", nullable: false),
                    TemplatesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateTemplateSenderList", x => new { x.SendersId, x.TemplatesId });
                    table.ForeignKey(
                        name: "FK_TemplateTemplateSenderList_TemplateSenderList_SendersId",
                        column: x => x.SendersId,
                        principalTable: "TemplateSenderList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemplateTemplateSenderList_Templates_TemplatesId",
                        column: x => x.TemplatesId,
                        principalTable: "Templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Distribution",
                keyColumn: "Id",
                keyValue: 1,
                column: "TemplateSenderListId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Distribution",
                keyColumn: "Id",
                keyValue: 2,
                column: "TemplateSenderListId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Distribution_TemplateSenderListId",
                table: "Distribution",
                column: "TemplateSenderListId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateTemplateSenderList_TemplatesId",
                table: "TemplateTemplateSenderList",
                column: "TemplatesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Distribution_TemplateSenderList_TemplateSenderListId",
                table: "Distribution",
                column: "TemplateSenderListId",
                principalTable: "TemplateSenderList",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Distribution_TemplateSenderList_TemplateSenderListId",
                table: "Distribution");

            migrationBuilder.DropTable(
                name: "TemplateTemplateSenderList");

            migrationBuilder.DropTable(
                name: "TemplateSenderList");

            migrationBuilder.DropIndex(
                name: "IX_Distribution_TemplateSenderListId",
                table: "Distribution");

            migrationBuilder.DropColumn(
                name: "TemplateSenderListId",
                table: "Distribution");

            migrationBuilder.AddColumn<bool>(
                name: "IsLastTimeSender",
                table: "Distribution",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TemplateId",
                table: "Distribution",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Distribution",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IsLastTimeSender", "TemplateId" },
                values: new object[] { false, 0 });

            migrationBuilder.UpdateData(
                table: "Distribution",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IsLastTimeSender", "TemplateId" },
                values: new object[] { false, 0 });
        }
    }
}
