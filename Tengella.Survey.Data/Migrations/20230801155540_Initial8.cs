using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tengella.Survey.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Distribution_TemplateSenderList_TemplateSenderListId",
                table: "Distribution");

            migrationBuilder.DropForeignKey(
                name: "FK_TemplateTemplateSenderList_TemplateSenderList_SendersId",
                table: "TemplateTemplateSenderList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TemplateSenderList",
                table: "TemplateSenderList");

            migrationBuilder.RenameTable(
                name: "TemplateSenderList",
                newName: "TemplateSenderLists");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TemplateSenderLists",
                table: "TemplateSenderLists",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Distribution_TemplateSenderLists_TemplateSenderListId",
                table: "Distribution",
                column: "TemplateSenderListId",
                principalTable: "TemplateSenderLists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TemplateTemplateSenderList_TemplateSenderLists_SendersId",
                table: "TemplateTemplateSenderList",
                column: "SendersId",
                principalTable: "TemplateSenderLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Distribution_TemplateSenderLists_TemplateSenderListId",
                table: "Distribution");

            migrationBuilder.DropForeignKey(
                name: "FK_TemplateTemplateSenderList_TemplateSenderLists_SendersId",
                table: "TemplateTemplateSenderList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TemplateSenderLists",
                table: "TemplateSenderLists");

            migrationBuilder.RenameTable(
                name: "TemplateSenderLists",
                newName: "TemplateSenderList");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TemplateSenderList",
                table: "TemplateSenderList",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Distribution_TemplateSenderList_TemplateSenderListId",
                table: "Distribution",
                column: "TemplateSenderListId",
                principalTable: "TemplateSenderList",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TemplateTemplateSenderList_TemplateSenderList_SendersId",
                table: "TemplateTemplateSenderList",
                column: "SendersId",
                principalTable: "TemplateSenderList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
