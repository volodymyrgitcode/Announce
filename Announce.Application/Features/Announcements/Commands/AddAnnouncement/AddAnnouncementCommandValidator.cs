using FluentValidation;

namespace Announce.Application.Features.Announcements.Commands.AddAnnouncement;

public class AddAnnouncementCommandValidator : AbstractValidator<AddAnnouncementCommand>
{
    public AddAnnouncementCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(2500).WithMessage("Description must not exceed 2500 characters.");
    }
}
