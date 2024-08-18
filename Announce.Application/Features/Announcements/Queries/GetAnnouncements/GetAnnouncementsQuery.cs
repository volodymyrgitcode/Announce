using Announce.Application.Common.DTOs;
using MediatR;

namespace Announce.Application.Features.Announcements.Queries.GetAnnouncements;

public record GetAnnouncementsQuery : IRequest<IEnumerable<AnnouncementDto>>
{
}
