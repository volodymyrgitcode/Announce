using Announce.Domain.Entities;

namespace Announce.Application.Common.Interfaces;

public interface IAnnouncementRepository : IRepository<Announcement, Guid>
{
    Task<IEnumerable<Announcement>> GetSimilar(Announcement announcement, int count);
}
