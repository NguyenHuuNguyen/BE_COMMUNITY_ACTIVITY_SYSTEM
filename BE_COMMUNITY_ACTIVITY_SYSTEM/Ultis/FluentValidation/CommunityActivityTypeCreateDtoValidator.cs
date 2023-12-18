using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.CommunityActivityType;
using FluentValidation;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Ultis.FluentValidation
{
    public class CommunityActivityTypeCreateDtoValidator : AbstractValidator<CommunityActivityTypeCreateDto>
    {
        public CommunityActivityTypeCreateDtoValidator()
        {
            RuleFor(x => x.MinScore)
                .InclusiveBetween(0, 700)
                .WithMessage(string.Format(Constants.ErrorMessages.INVALID_MINSCORE, "MinScore"));

            RuleFor(x => x.MaxScore)
                .InclusiveBetween(0, 700)
                .WithMessage(string.Format(Constants.ErrorMessages.INVALID_MAXSCORE, "MaxScore"))
                .Must((dto, maxScore) => maxScore >= dto.MinScore)
                .WithMessage(string.Format(Constants.ErrorMessages.MAXSCORE_MUST_BE_GREATER_THAN_MINSCORE, "MaxScore"));

        }
    }
}
