using Announce.Application.Common.DTOs;
using Announce.Application.Common.Interfaces;
using Announce.Application.Common.Exceptions;
using AutoMapper;
using MediatR;
using Announce.Domain.Entities;

namespace Announce.Application.Features.Announcements.Queries.GetAnnouncement;

public class GetAnnouncementQueryHandler : IRequestHandler<GetAnnouncementQuery, AnnouncementDto>
{
    private readonly IAnnouncementRepository _announcementRepository;
    private readonly IMapper _mapper;
    public GetAnnouncementQueryHandler(IAnnouncementRepository announcementRepository, IMapper mapper)
    {
        _announcementRepository = announcementRepository;
        _mapper = mapper;
    }
    public async Task<AnnouncementDto> Handle(GetAnnouncementQuery request, CancellationToken cancellationToken)
    {
        var existingAnnouncement = await _announcementRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(Announcement), request.Id);

        var announcementDto = _mapper.Map<AnnouncementDto>(existingAnnouncement);

        return announcementDto;
    }
}
