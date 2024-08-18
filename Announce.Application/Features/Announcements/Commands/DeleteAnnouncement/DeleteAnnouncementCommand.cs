using MediatR;

namespace Announce.Application.Features.Announcements.Commands.DeleteAnnouncement;

public record DeleteAnnouncementCommand : IRequest
{
    public Guid Id { get; set; }
}
