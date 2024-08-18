using Announce.Application.Common.DTOs;
using Announce.Application.Common.Interfaces;
using Announce.Domain.Entities;
using AutoMapper;
using MediatR;

namespace Announce.Application.Features.Announcements.Commands.AddAnnouncement;

public class AddAnnouncementCommandHandler : IRequestHandler<AddAnnouncementCommand, AnnouncementDto>
{
    private readonly IAnnouncementRepository _announcementRepository;
    private readonly IMapper _mapper;
    public AddAnnouncementCommandHandler(IAnnouncementRepository announcementRepository, IMapper mapper)
    {
        _announcementRepository = announcementRepository;
        _mapper = mapper;
    }
    public async Task<AnnouncementDto> Handle(AddAnnouncementCommand request, CancellationToken cancellationToken)
    {
        var announcementEntity = _mapper.Map<Announcement>(request);

        var createdAnnouncement = await _announcementRepository.AddAsync(announcementEntity);

        return _mapper.Map<AnnouncementDto>(createdAnnouncement);
    }
}
