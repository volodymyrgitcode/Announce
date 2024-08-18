using Announce.Application.Common.DTOs;
using Announce.Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace Announce.Application.Features.Announcements.Queries.GetAnnouncements;

public class GetAnnouncementsQueryHandler : IRequestHandler<GetAnnouncementsQuery, IEnumerable<AnnouncementDto>>
{
    private readonly IAnnouncementRepository _announcementRepository;
    private readonly IMapper _mapper;
    public GetAnnouncementsQueryHandler(IAnnouncementRepository announcementRepository, IMapper mapper)
    {
        _announcementRepository = announcementRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AnnouncementDto>> Handle(GetAnnouncementsQuery request, CancellationToken cancellationToken)
    {
        var announcements = await _announcementRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<AnnouncementDto>>(announcements);
    }
}
