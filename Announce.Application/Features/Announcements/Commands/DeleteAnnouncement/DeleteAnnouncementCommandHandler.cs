using Announce.Application.Common.Exceptions;
using Announce.Application.Common.Interfaces;
using Announce.Domain.Entities;
using MediatR;

namespace Announce.Application.Features.Announcements.Commands.DeleteAnnouncement;

public class DeleteAnnouncementCommandHandler : IRequestHandler<DeleteAnnouncementCommand>
{
    private readonly IAnnouncementRepository _announcementRepository;
    public DeleteAnnouncementCommandHandler(IAnnouncementRepository announcementRepository)
    {
        _announcementRepository = announcementRepository;
    }
    public async Task Handle(DeleteAnnouncementCommand request, CancellationToken cancellationToken)
    {
        var existingAnnouncement = await _announcementRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(Announcement), request.Id);

        await _announcementRepository.DeleteAsync(existingAnnouncement);
    }
}
