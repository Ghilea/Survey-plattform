﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tengella.Survey.Data;

#nullable disable

namespace Tengella.Survey.Data.Migrations
{
    [DbContext(typeof(SurveyDbContext))]
    partial class SurveyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StatisticStatisticQuestion", b =>
                {
                    b.Property<int>("QuestionsId")
                        .HasColumnType("int");

                    b.Property<int>("StatisticsId")
                        .HasColumnType("int");

                    b.HasKey("QuestionsId", "StatisticsId");

                    b.HasIndex("StatisticsId");

                    b.ToTable("StatisticStatisticQuestion");
                });

            modelBuilder.Entity("StatisticSurveyList", b =>
                {
                    b.Property<int>("StatisticsId")
                        .HasColumnType("int");

                    b.Property<int>("SurveyListsId")
                        .HasColumnType("int");

                    b.HasKey("StatisticsId", "SurveyListsId");

                    b.HasIndex("SurveyListsId");

                    b.ToTable("StatisticSurveyList");
                });

            modelBuilder.Entity("SurveyListSurveyQuestion", b =>
                {
                    b.Property<int>("QuestionsId")
                        .HasColumnType("int");

                    b.Property<int>("SurveyListsId")
                        .HasColumnType("int");

                    b.HasKey("QuestionsId", "SurveyListsId");

                    b.HasIndex("SurveyListsId");

                    b.ToTable("SurveyListSurveyQuestion");
                });

            modelBuilder.Entity("SurveyOptionSurveyQuestion", b =>
                {
                    b.Property<int>("OptionsId")
                        .HasColumnType("int");

                    b.Property<int>("SurveyQuestionsId")
                        .HasColumnType("int");

                    b.HasKey("OptionsId", "SurveyQuestionsId");

                    b.HasIndex("SurveyQuestionsId");

                    b.ToTable("SurveyOptionSurveyQuestion");
                });

            modelBuilder.Entity("TemplateTemplateSenderList", b =>
                {
                    b.Property<int>("SendersId")
                        .HasColumnType("int");

                    b.Property<int>("TemplatesId")
                        .HasColumnType("int");

                    b.HasKey("SendersId", "TemplatesId");

                    b.HasIndex("TemplatesId");

                    b.ToTable("TemplateTemplateSenderList");
                });

            modelBuilder.Entity("Tengella.Survey.Data.Models.Distribution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DistributionTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsToRecive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrganizationNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StatisticId")
                        .HasColumnType("int");

                    b.Property<int?>("TemplateSenderListId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DistributionTypeId");

                    b.HasIndex("StatisticId");

                    b.HasIndex("TemplateSenderListId");

                    b.ToTable("Distribution");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DistributionTypeId = 1,
                            Email = "coleman.windler95@ethereal.email",
                            IsToRecive = true,
                            Name = "Coleman Windler"
                        },
                        new
                        {
                            Id = 2,
                            DistributionTypeId = 2,
                            Email = "blizzard@company.com",
                            IsToRecive = true,
                            Name = "Activision Blizzard",
                            OrganizationNumber = "230104"
                        });
                });

            modelBuilder.Entity("Tengella.Survey.Data.Models.DistributionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DistributionTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Privatperson"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Företag"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Offentlig verksamhet"
                        });
                });

            modelBuilder.Entity("Tengella.Survey.Data.Models.Statistic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int>("DistributionId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDone")
                        .HasColumnType("bit");

                    b.Property<int>("SurveyListId")
                        .HasColumnType("int");

                    b.Property<int>("TemplateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Statistics");
                });

            modelBuilder.Entity("Tengella.Survey.Data.Models.StatisticQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Answer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StatisticId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("StatisticsQuestions");
                });

            modelBuilder.Entity("Tengella.Survey.Data.Models.SurveyList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SurveyTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SurveyTypeId");

                    b.ToTable("SurveyList");
                });

            modelBuilder.Entity("Tengella.Survey.Data.Models.SurveyOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsLimit")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SurveyQuestionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SurveyOptions");
                });

            modelBuilder.Entity("Tengella.Survey.Data.Models.SurveyQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AdditionalInfo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SurveyListId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SurveyQuestions");
                });

            modelBuilder.Entity("Tengella.Survey.Data.Models.SurveyType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SurveyTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Kundundersökning"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Uppdragsundersökning"
                        });
                });

            modelBuilder.Entity("Tengella.Survey.Data.Models.Template", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StatisticId")
                        .HasColumnType("int");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SurveyId")
                        .HasColumnType("int");

                    b.Property<int?>("SurveyListsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StatisticId");

                    b.HasIndex("SurveyListsId");

                    b.ToTable("Templates");
                });

            modelBuilder.Entity("Tengella.Survey.Data.Models.TemplateSenderList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DistributionId")
                        .HasColumnType("int");

                    b.Property<int>("TemplateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TemplateSenderLists");
                });

            modelBuilder.Entity("StatisticStatisticQuestion", b =>
                {
                    b.HasOne("Tengella.Survey.Data.Models.StatisticQuestion", null)
                        .WithMany()
                        .HasForeignKey("QuestionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tengella.Survey.Data.Models.Statistic", null)
                        .WithMany()
                        .HasForeignKey("StatisticsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StatisticSurveyList", b =>
                {
                    b.HasOne("Tengella.Survey.Data.Models.Statistic", null)
                        .WithMany()
                        .HasForeignKey("StatisticsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tengella.Survey.Data.Models.SurveyList", null)
                        .WithMany()
                        .HasForeignKey("SurveyListsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SurveyListSurveyQuestion", b =>
                {
                    b.HasOne("Tengella.Survey.Data.Models.SurveyQuestion", null)
                        .WithMany()
                        .HasForeignKey("QuestionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tengella.Survey.Data.Models.SurveyList", null)
                        .WithMany()
                        .HasForeignKey("SurveyListsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SurveyOptionSurveyQuestion", b =>
                {
                    b.HasOne("Tengella.Survey.Data.Models.SurveyOption", null)
                        .WithMany()
                        .HasForeignKey("OptionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tengella.Survey.Data.Models.SurveyQuestion", null)
                        .WithMany()
                        .HasForeignKey("SurveyQuestionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TemplateTemplateSenderList", b =>
                {
                    b.HasOne("Tengella.Survey.Data.Models.TemplateSenderList", null)
                        .WithMany()
                        .HasForeignKey("SendersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tengella.Survey.Data.Models.Template", null)
                        .WithMany()
                        .HasForeignKey("TemplatesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tengella.Survey.Data.Models.Distribution", b =>
                {
                    b.HasOne("Tengella.Survey.Data.Models.DistributionType", "DistributionTypes")
                        .WithMany()
                        .HasForeignKey("DistributionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tengella.Survey.Data.Models.Statistic", null)
                        .WithMany("Distributions")
                        .HasForeignKey("StatisticId");

                    b.HasOne("Tengella.Survey.Data.Models.TemplateSenderList", null)
                        .WithMany("Distributions")
                        .HasForeignKey("TemplateSenderListId");

                    b.Navigation("DistributionTypes");
                });

            modelBuilder.Entity("Tengella.Survey.Data.Models.SurveyList", b =>
                {
                    b.HasOne("Tengella.Survey.Data.Models.SurveyType", "SurveyTypes")
                        .WithMany()
                        .HasForeignKey("SurveyTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SurveyTypes");
                });

            modelBuilder.Entity("Tengella.Survey.Data.Models.Template", b =>
                {
                    b.HasOne("Tengella.Survey.Data.Models.Statistic", null)
                        .WithMany("Templates")
                        .HasForeignKey("StatisticId");

                    b.HasOne("Tengella.Survey.Data.Models.SurveyList", "SurveyLists")
                        .WithMany()
                        .HasForeignKey("SurveyListsId");

                    b.Navigation("SurveyLists");
                });

            modelBuilder.Entity("Tengella.Survey.Data.Models.Statistic", b =>
                {
                    b.Navigation("Distributions");

                    b.Navigation("Templates");
                });

            modelBuilder.Entity("Tengella.Survey.Data.Models.TemplateSenderList", b =>
                {
                    b.Navigation("Distributions");
                });
#pragma warning restore 612, 618
        }
    }
}
