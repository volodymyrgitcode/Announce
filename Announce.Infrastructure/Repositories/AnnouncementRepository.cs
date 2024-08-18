using Announce.Application.Common.Interfaces;
using Announce.Domain.Entities;
using Announce.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Announce.Infrastructure.Repositories;

public class AnnouncementRepository : IAnnouncementRepository
{
    private readonly ApplicationDbContext _dbContext;

    public AnnouncementRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    public async Task<IEnumerable<Announcement>> GetSimilar(Announcement announcement, int count)
    {
        var words = GetWords(announcement.Title + ' ' + announcement.Description);

        var similarAnnouncements = await _dbContext.Announcements
            .AsNoTracking()
            .Where(a => a.Id != announcement.Id)
            .Select(a => new 
                {
                    Announcement = a,
                    SimilarityScore = words.Count(w => (a.Title + ' ' + a.Description).Contains(w)),
                })
            .Where(a => a.SimilarityScore > 0)
            .OrderByDescending(a => a.SimilarityScore)
            .Take(count)
            .Select(a => a.Announcement)
            .ToListAsync();

        return similarAnnouncements;
    }

    private HashSet<string> GetWords(string text)
    {
        return new HashSet<string>(
            text.ToLowerInvariant()
                .Split(new[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries)
                .Where(word => word.Length > 2)
        );
    }

    public async Task<IEnumerable<Announcement>> GetAllAsync()
    {
        return await _dbContext.Announcements.AsNoTracking().ToListAsync();
    }

    public async Task<Announcement?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Announcements.FindAsync(id);
    }

    public async Task<Announcement> AddAsync(Announcement entity)
    {
        SetAuditFieldsForCreation(entity, DateTime.UtcNow);
        var createdAnnouncement = await _dbContext.Announcements.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return createdAnnouncement.Entity;
    }

    public void Delete(Announcement entity)
    {
        _dbContext.Announcements.Remove(entity);
        _dbContext.SaveChanges();
    }

    public async Task DeleteAsync(Announcement entity)
    {
        _dbContext.Announcements.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Announcement entity)
    {
        SetAuditFieldsForUpdate(entity, DateTime.UtcNow);
        _dbContext.Announcements.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    private void SetAuditFieldsForCreation(Announcement entity, DateTime dateTime)
    {
        entity.CreatedAt = dateTime;
        SetAuditFieldsForUpdate(entity, dateTime);
    }

    private void SetAuditFieldsForUpdate(Announcement entity, DateTime dateTime)
    {
        entity.UpdatedAt = dateTime;
    }
}
