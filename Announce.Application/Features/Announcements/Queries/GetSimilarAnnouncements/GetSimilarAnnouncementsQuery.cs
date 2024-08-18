using Announce.Application.Common.DTOs;
using MediatR;

namespace Announce.Application.Features.Announcements.Queries.GetSimilarAnnouncements;

public record GetSimilarAnnouncementsQuery : IRequest<IEnumerable<AnnouncementDto>>
{
    public Guid Id { get; set; }
    public int Count { get; init; }
}
