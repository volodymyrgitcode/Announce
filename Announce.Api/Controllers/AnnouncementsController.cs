using Announce.Application.Common.DTOs;
using Announce.Application.Features.Announcements.Commands.AddAnnouncement;
using Announce.Application.Features.Announcements.Commands.DeleteAnnouncement;
using Announce.Application.Features.Announcements.Commands.UpdateAnnouncement;
using Announce.Application.Features.Announcements.Queries.GetAnnouncement;
using Announce.Application.Features.Announcements.Queries.GetAnnouncements;
using Announce.Application.Features.Announcements.Queries.GetSimilarAnnouncements;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Announce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnnouncementsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AnnouncementsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}/similar")]
    public async Task<ActionResult<IEnumerable<AnnouncementDto>>> GetSimilar([FromRoute] Guid id, [FromQuery] int count = 3)
    {
        var similarAnnouncements = await _mediator.Send(new GetSimilarAnnouncementsQuery { Id = id, Count = count});
        return Ok(similarAnnouncements);
    }

    [HttpGet()]
    public async Task<ActionResult<AnnouncementDto>> GetAll()
    {
        var announcementDTOs = await _mediator.Send(new GetAnnouncementsQuery());
        return Ok(announcementDTOs);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AnnouncementDto>> Get([FromRoute] Guid id)
    {
        var announcementDto =  await _mediator.Send(new GetAnnouncementQuery { Id = id });
        return Ok(announcementDto);
    }

    [HttpPost]
    public async Task<ActionResult<AnnouncementDto>> Create([FromBody] AddAnnouncementCommand command)
    {
        var createdAnnouncementDto = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id = createdAnnouncementDto.Id }, createdAnnouncementDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateAnnouncementCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _mediator.Send(new DeleteAnnouncementCommand { Id = id });
        return NoContent();
    }
}
