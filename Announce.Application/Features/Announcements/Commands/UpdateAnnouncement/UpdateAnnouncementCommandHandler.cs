using Announce.Application.Common.Exceptions;
using Announce.Application.Common.Interfaces;
using Announce.Domain.Entities;
using AutoMapper;
using MediatR;

namespace Announce.Application.Features.Announcements.Commands.UpdateAnnouncement;

public class UpdateAnnouncementCommandHandler : IRequestHandler<UpdateAnnouncementCommand>
{
    private readonly IAnnouncementRepository _announcementRepository;
    private readonly IMapper _mapper;
    public UpdateAnnouncementCommandHandler(IAnnouncementRepository announcementRepository, IMapper mapper)
    {
        _announcementRepository = announcementRepository;
        _mapper = mapper;
    }
    public async Task Handle(UpdateAnnouncementCommand request, CancellationToken cancellationToken)
    {
        var announcement = await _announcementRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(Announcement), request.Id);

        _mapper.Map(request, announcement);

        await _announcementRepository.UpdateAsync(announcement);
    }
}
