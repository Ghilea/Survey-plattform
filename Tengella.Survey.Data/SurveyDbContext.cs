using Microsoft.EntityFrameworkCore;
using Tengella.Survey.Data.Models;

namespace Tengella.Survey.Data;

public class SurveyDbContext : DbContext
{
    public SurveyDbContext(DbContextOptions<SurveyDbContext> options) : base(options)
    {
    }

    public DbSet<SurveyList> SurveyList { get; set; }
    public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
    public DbSet<SurveyOption> SurveyOptions { get; set; }
    public DbSet<SurveyType> SurveyTypes { get; set; }
    public DbSet<Template> Templates { get; set; }
    public DbSet<TemplateSenderList> TemplateSenderLists { get; set; }
    public DbSet<Distribution> Distribution { get; set; }
    public DbSet<DistributionType> DistributionTypes { get; set; }
    public DbSet<Statistic> Statistics { get; set; }
    public DbSet<StatisticQuestion> StatisticsQuestions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {        
        // Seeding SurveyType
        modelBuilder.Entity<SurveyType>().HasData(
            new SurveyType { Id = 1, Name = "Kundundersökning" },
            new SurveyType { Id = 2, Name = "Uppdragsundersökning" }
        );

        // Seeding DistributionTypes
        modelBuilder.Entity<DistributionType>().HasData(
            new DistributionType
            {
                Id = 1,
                Name = "Privatperson",
            },
            new DistributionType
            {
                Id = 2,
                Name = "Företag"
            },
            new DistributionType
            {
                Id = 3,
                Name = "Offentlig verksamhet"
            }
        );

        // Seeding EmailDistributionList
        modelBuilder.Entity<Distribution>().HasData(
            new Distribution
            {
                Id = 1,
                DistributionTypeId = 1,
                Name = "Coleman Windler",
                Email = "coleman.windler95@ethereal.email",
                IsToRecive = true,
            },
            new Distribution
            {
                Id = 2,
                DistributionTypeId = 2,
                OrganizationNumber = "230104",
                Name = "Activision Blizzard",
                Email = "blizzard@company.com",
                IsToRecive = true,
            }
        );
    }
}
