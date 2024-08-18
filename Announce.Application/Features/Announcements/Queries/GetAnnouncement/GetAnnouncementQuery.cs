using Announce.Application.Common.DTOs;
using MediatR;

namespace Announce.Application.Features.Announcements.Queries.GetAnnouncement;

public record GetAnnouncementQuery : IRequest<AnnouncementDto>
{
    public Guid Id { get; init; }
}
