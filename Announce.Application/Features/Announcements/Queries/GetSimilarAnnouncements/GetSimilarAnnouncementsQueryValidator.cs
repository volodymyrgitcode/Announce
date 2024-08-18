using FluentValidation;

namespace Announce.Application.Features.Announcements.Queries.GetSimilarAnnouncements;

public class GetSimilarAnnouncementsQueryValidator : AbstractValidator<GetSimilarAnnouncementsQuery>
{
    public GetSimilarAnnouncementsQueryValidator()
    {
        RuleFor(x => x.Count)
             .GreaterThan(0).WithMessage("Count must be greater than 0.")
             .LessThanOrEqualTo(100).WithMessage("Count must not exceed 100.");
    }
}