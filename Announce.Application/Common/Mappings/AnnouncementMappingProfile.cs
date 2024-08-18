using Announce.Application.Common.DTOs;
using Announce.Application.Features.Announcements.Commands.AddAnnouncement;
using Announce.Application.Features.Announcements.Commands.UpdateAnnouncement;
using Announce.Domain.Entities;
using AutoMapper;

namespace Announce.Application.Common.Mappings;

public class AnnouncementMappingProfile : Profile
{
    public AnnouncementMappingProfile()
    {
        CreateMap<Announcement, AnnouncementDto>()
           .ForMember(dto => dto.CreatedAt, opt => opt.MapFrom(entity => entity.CreatedAt.DateTime));

 /*       CreateMap<AnnouncementDto, Announcement>()
            .ForMember(entity => entity.CreatedAt, opt => opt.MapFrom(dto => new DateTimeOffset(dto.CreatedAt)));*/

        CreateMap<AddAnnouncementCommand, Announcement>();
        CreateMap<UpdateAnnouncementCommand, Announcement>();
    }
}
