using Announce.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Announce.Infrastructure.Data.Contexts;

public static class InitializerExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();

        await initializer.InitializeAsync();

        await initializer.SeedAsync();
    }
}

public class ApplicationDbContextInitializer
{
    private readonly ILogger<ApplicationDbContextInitializer> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitializer(ILogger<ApplicationDbContextInitializer> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitializeAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initializing the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        if (!_context.Announcements.Any())
        {
            var announcements = new List<Announcement>
            {
                new Announcement
                {
                    Id = Guid.NewGuid(),
                    Title = "System Upgrade Notice",
                    Description = "Our system will undergo a major upgrade this fall. Expect improved performance and new features.",
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                },
                new Announcement
                {
                    Id = Guid.NewGuid(),
                    Title = "Team Expansion",
                    Description = "We are thrilled to announce that we have added 5 new team members to our engineering department.",
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                },
                new Announcement
                {
                    Id = Guid.NewGuid(),
                    Title = "Customer Satisfaction Survey",
                    Description = "We value your feedback! Please take a moment to complete our customer satisfaction survey.",
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                },
                new Announcement
                {
                    Id = Guid.NewGuid(),
                    Title = "Webinar Invitation",
                    Description = "Join us for an exclusive webinar on the latest trends in technology coming soon.",
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                }
            };
            _context.Announcements.AddRange(announcements);
            await _context.SaveChangesAsync();
        }
    }
}
