using Announce.Application.Common.DTOs;
using Announce.Application.Common.Exceptions;
using Announce.Application.Common.Interfaces;
using Announce.Domain.Entities;
using AutoMapper;
using MediatR;

namespace Announce.Application.Features.Announcements.Queries.GetSimilarAnnouncements;

public class GetSimilarAnnouncementsQueryHandler : IRequestHandler<GetSimilarAnnouncementsQuery, IEnumerable<AnnouncementDto>>
{
    private readonly IAnnouncementRepository _announcementRepository;
    private readonly IMapper _mapper;
    public GetSimilarAnnouncementsQueryHandler(IAnnouncementRepository announcementRepository, IMapper mapper)
    {
        _announcementRepository = announcementRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AnnouncementDto>> Handle(GetSimilarAnnouncementsQuery request, CancellationToken cancellationToken)
    {
        var existingAnnouncement = await _announcementRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(Announcement), request.Id);

        var similarAnnouncements = await _announcementRepository.GetSimilar(existingAnnouncement, request.Count);

        return _mapper.Map<IEnumerable<AnnouncementDto>>(similarAnnouncements);
    }
}
