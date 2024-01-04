using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.CommunityActivityType;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using FluentValidation;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Ultis.FluentValidation
{
    public class CommunityActivityTypeCreateDtoValidator : AbstractValidator<CommunityActivityTypeCreateDto>
    {
        private readonly ICommunityActivityTypeRepository _communityActivityTypeRepository;

        public CommunityActivityTypeCreateDtoValidator(ICommunityActivityTypeRepository communityActivityTypeRepository)
        {
            _communityActivityTypeRepository = communityActivityTypeRepository;

            RuleFor(x => x.MinScore)
                .InclusiveBetween(0, 700)
                .WithMessage(Constants.ErrorMessages.INVALID_MINSCORE);

            RuleFor(x => x.MaxScore)
                .InclusiveBetween(0, 700)
                .WithMessage(Constants.ErrorMessages.INVALID_MAXSCORE)
                .Must((dto, maxScore) => maxScore >= dto.MinScore)
                .WithMessage(Constants.ErrorMessages.MAXSCORE_MUST_BE_GREATER_THAN_MINSCORE);

            RuleFor(x => x.Name)
                .Must(name =>  name == null || _communityActivityTypeRepository.CheckCommunityActivityTypeNameExists(name) == false)
                .WithMessage(string.Format(Constants.ErrorMessages.ALREADY_EXISTS, "Name"));
        }
    }
}
