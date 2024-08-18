using MediatR;

namespace Announce.Application.Features.Announcements.Commands.UpdateAnnouncement;

public record UpdateAnnouncementCommand : IRequest
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}