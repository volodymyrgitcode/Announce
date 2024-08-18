using Announce.Domain.Common;

namespace Announce.Domain.Entities;

public class Announcement : BaseAuditableEntity<Guid>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
