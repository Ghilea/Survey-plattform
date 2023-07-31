using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tengella.Survey.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DistributionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurveyListId = table.Column<int>(type: "int", nullable: false),
                    DistributionId = table.Column<int>(type: "int", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatisticsQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatisticId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticsQuestions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SurveyOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsChecked = table.Column<bool>(type: "bit", nullable: false),
                    SurveyQuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SurveyQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdditionalInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SurveyListId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyQuestions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SurveyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Distribution",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistributionTypeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganizationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsToRecive = table.Column<bool>(type: "bit", nullable: false),
                    StatisticId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distribution", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Distribution_DistributionTypes_DistributionTypeId",
                        column: x => x.DistributionTypeId,
                        principalTable: "DistributionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Distribution_Statistics_StatisticId",
                        column: x => x.StatisticId,
                        principalTable: "Statistics",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StatisticStatisticQuestion",
                columns: table => new
                {
                    QuestionsId = table.Column<int>(type: "int", nullable: false),
                    StatisticsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticStatisticQuestion", x => new { x.QuestionsId, x.StatisticsId });
                    table.ForeignKey(
                        name: "FK_StatisticStatisticQuestion_StatisticsQuestions_QuestionsId",
                        column: x => x.QuestionsId,
                        principalTable: "StatisticsQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StatisticStatisticQuestion_Statistics_StatisticsId",
                        column: x => x.StatisticsId,
                        principalTable: "Statistics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SurveyOptionSurveyQuestion",
                columns: table => new
                {
                    OptionsId = table.Column<int>(type: "int", nullable: false),
                    SurveyQuestionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyOptionSurveyQuestion", x => new { x.OptionsId, x.SurveyQuestionsId });
                    table.ForeignKey(
                        name: "FK_SurveyOptionSurveyQuestion_SurveyOptions_OptionsId",
                        column: x => x.OptionsId,
                        principalTable: "SurveyOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyOptionSurveyQuestion_SurveyQuestions_SurveyQuestionsId",
                        column: x => x.SurveyQuestionsId,
                        principalTable: "SurveyQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SurveyList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SurveyTypeId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyList_SurveyTypes_SurveyTypeId",
                        column: x => x.SurveyTypeId,
                        principalTable: "SurveyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatisticSurveyList",
                columns: table => new
                {
                    StatisticsId = table.Column<int>(type: "int", nullable: false),
                    SurveyListsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticSurveyList", x => new { x.StatisticsId, x.SurveyListsId });
                    table.ForeignKey(
                        name: "FK_StatisticSurveyList_Statistics_StatisticsId",
                        column: x => x.StatisticsId,
                        principalTable: "Statistics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StatisticSurveyList_SurveyList_SurveyListsId",
                        column: x => x.SurveyListsId,
                        principalTable: "SurveyList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SurveyListSurveyQuestion",
                columns: table => new
                {
                    QuestionsId = table.Column<int>(type: "int", nullable: false),
                    SurveyListsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyListSurveyQuestion", x => new { x.QuestionsId, x.SurveyListsId });
                    table.ForeignKey(
                        name: "FK_SurveyListSurveyQuestion_SurveyList_SurveyListsId",
                        column: x => x.SurveyListsId,
                        principalTable: "SurveyList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyListSurveyQuestion_SurveyQuestions_QuestionsId",
                        column: x => x.QuestionsId,
                        principalTable: "SurveyQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Templates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SurveyId = table.Column<int>(type: "int", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SurveyListsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Templates_SurveyList_SurveyListsId",
                        column: x => x.SurveyListsId,
                        principalTable: "SurveyList",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "DistributionTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Privatperson" },
                    { 2, "Företag" },
                    { 3, "Offentlig verksamhet" }
                });

            migrationBuilder.InsertData(
                table: "SurveyTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Kundundersökning" },
                    { 2, "Uppdragsundersökning" }
                });

            migrationBuilder.InsertData(
                table: "Distribution",
                columns: new[] { "Id", "DistributionTypeId", "Email", "IsToRecive", "Name", "OrganizationNumber", "StatisticId" },
                values: new object[,]
                {
                    { 1, 1, "coleman.windler95@ethereal.email", true, "Coleman Windler", null, null },
                    { 2, 2, "blizzard@company.com", true, "Activision Blizzard", "230104", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Distribution_DistributionTypeId",
                table: "Distribution",
                column: "DistributionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Distribution_StatisticId",
                table: "Distribution",
                column: "StatisticId");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticStatisticQuestion_StatisticsId",
                table: "StatisticStatisticQuestion",
                column: "StatisticsId");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticSurveyList_SurveyListsId",
                table: "StatisticSurveyList",
                column: "SurveyListsId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyList_SurveyTypeId",
                table: "SurveyList",
                column: "SurveyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyListSurveyQuestion_SurveyListsId",
                table: "SurveyListSurveyQuestion",
                column: "SurveyListsId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyOptionSurveyQuestion_SurveyQuestionsId",
                table: "SurveyOptionSurveyQuestion",
                column: "SurveyQuestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_SurveyListsId",
                table: "Templates",
                column: "SurveyListsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Distribution");

            migrationBuilder.DropTable(
                name: "StatisticStatisticQuestion");

            migrationBuilder.DropTable(
                name: "StatisticSurveyList");

            migrationBuilder.DropTable(
                name: "SurveyListSurveyQuestion");

            migrationBuilder.DropTable(
                name: "SurveyOptionSurveyQuestion");

            migrationBuilder.DropTable(
                name: "Templates");

            migrationBuilder.DropTable(
                name: "DistributionTypes");

            migrationBuilder.DropTable(
                name: "StatisticsQuestions");

            migrationBuilder.DropTable(
                name: "Statistics");

            migrationBuilder.DropTable(
                name: "SurveyOptions");

            migrationBuilder.DropTable(
                name: "SurveyQuestions");

            migrationBuilder.DropTable(
                name: "SurveyList");

            migrationBuilder.DropTable(
                name: "SurveyTypes");
        }
    }
}
