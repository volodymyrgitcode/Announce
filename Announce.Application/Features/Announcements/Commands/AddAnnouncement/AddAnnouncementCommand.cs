using Announce.Application.Common.DTOs;
using MediatR;

namespace Announce.Application.Features.Announcements.Commands.AddAnnouncement;

public record AddAnnouncementCommand : IRequest<AnnouncementDto>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
