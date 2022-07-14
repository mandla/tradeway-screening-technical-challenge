using Microsoft.EntityFrameworkCore;

namespace DemoWebAPI.Models;

public class DemoContext : DbContext
{
    public DemoContext(DbContextOptions<DemoContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<QuestionOption>()
            .HasOne(o => o.Question)
            .WithMany(q => q.QuestionOptions)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder
            .Entity<QuestionAnswer>()
            .HasOne(qa => qa.QuizResponse)
            .WithMany( qr => qr.QuestionAnswers)
            .OnDelete(DeleteBehavior.NoAction);

        
        Seed(modelBuilder);
    }

    public virtual DbSet<Question> Questions => Set<Question>();
    public virtual DbSet<QuestionAnswer> QuestionAnswers => Set<QuestionAnswer>();
    public virtual DbSet<QuestionOption> QuestionOptions => Set<QuestionOption>();
    public virtual DbSet<QuizResponse> QuizResponses => Set<QuizResponse>();

    private void Seed(ModelBuilder mb)
    {
        SeedQuestions(mb);
        SeedOptions(mb);
        SeedAnswers(mb);
    }

    private void SeedQuestions(ModelBuilder mb)
    {
        mb.Entity<Question>()
            .HasData(
                new Question
                {
                    Id = 1,
                    Text = "One year for humans is the same as how many years for dogs?"
                },
                new Question
                {
                    Id = 2,
                    Text = "What is a good resting heart rate?"
                },
                new Question
                {
                    Id = 3,
                    Text = "If you were born in 2000 how old would you be now?"
                },
                new Question
                {
                    Id = 4,
                    Text = "Which of these sequences is the fibonacci sequence?"
                }
            );
    }

    private void SeedOptions(ModelBuilder mb)
    {
        mb.Entity<QuestionOption>()
            .HasData(
                // Question 1
                new QuestionOption
                {
                    Id = 1,
                    QuestionId = 1,
                    Text = "3 Years",
                    IsCorrect = false
                },
                new QuestionOption
                {
                    Id = 2,
                    QuestionId = 1,
                    Text = "5 Years",
                    IsCorrect = false
                },
                new QuestionOption
                {
                    Id = 3,
                    QuestionId = 1,
                    Text = "7 Years",
                    IsCorrect = true
                },
                // Question 2
                new QuestionOption
                {
                    Id = 4,
                    QuestionId = 2,
                    Text = "40",
                    IsCorrect = false
                },
                new QuestionOption
                {
                    Id = 5,
                    QuestionId = 2,
                    Text = "65",
                    IsCorrect = true
                },
                new QuestionOption
                {
                    Id = 6,
                    QuestionId = 2,
                    Text = "120",
                    IsCorrect = false,
                },
                // Question 3
                new QuestionOption
                {
                    Id = 7,
                    QuestionId = 3,
                    Text = "15",
                    IsCorrect = false
                },
                new QuestionOption
                {
                    Id = 8,
                    QuestionId = 3,
                    Text = "22",
                    IsCorrect = true
                },
                new QuestionOption
                {
                    Id = 9,
                    QuestionId = 3,
                    Text = "27",
                    IsCorrect = false,
                },
                // Question 4
                new QuestionOption
                {
                    Id = 10,
                    QuestionId = 4,
                    Text = "1, 2, 3, 4",
                    IsCorrect = false
                },
                new QuestionOption
                {
                    Id = 11,
                    QuestionId = 4,
                    Text = "1, 3, 5, 7",
                    IsCorrect = false
                },
                new QuestionOption
                {
                    Id = 12,
                    QuestionId = 4,
                    Text = "1, 1, 2, 3",
                    IsCorrect = true,
                }
            );
    }

    private void SeedAnswers(ModelBuilder mb)
    {
        var r = new Random();
        var qaId = 1;
        for (int qrId = 1; qrId <= 5000; qrId++)
        {
            mb.Entity<QuizResponse>()
                .HasData(
                    new QuizResponse
                    {
                        Id = qrId,
                        UserName = qrId.ToString()
                    }
                );

            for (int i = 0; i < 4; i++)
            {
                mb.Entity<QuestionAnswer>()
                    .HasData(
                        new QuestionAnswer
                        {
                            Id = qaId++,
                            QuizResponseId = qrId,
                            QuestionId = r.Next(1, 4),
                            QuestionOptionId = r.Next(1, 3)
                        }
                    );
            }
        }
    }
}