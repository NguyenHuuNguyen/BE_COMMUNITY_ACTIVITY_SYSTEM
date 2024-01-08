using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.CommunityActivity;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using FluentValidation;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Ultis.FluentValidation
{
    public class CommunityActivityCreateDtoValidator : AbstractValidator<CommunityActivityCreateDto>
    {
        private readonly ICommonRepository _commonRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICommunityActivityTypeRepository _communityActivityTypeRepository;

        public CommunityActivityCreateDtoValidator(ICommonRepository commonRepository, IUserRepository userRepository, ICommunityActivityTypeRepository communityActivityTypeRepository)
        {
            _commonRepository = commonRepository;
            _userRepository = userRepository;
            _communityActivityTypeRepository = communityActivityTypeRepository;

            RuleFor(x => x.UserId)
                .Must(userId => userId == null || _commonRepository.IsGuid(userId))
                .WithMessage(string.Format(Constants.ErrorMessages.INVALID_GUID, "UserId"))
                .Must(userId => userId == null || _userRepository.CheckUserExists(userId))
                .WithMessage(string.Format(Constants.ErrorMessages.NOT_FOUND, "User"));

            RuleFor(x => x.ActivityTypeId)
                .Must(activityTypeId => activityTypeId == null || _commonRepository.IsGuid(activityTypeId))
                .WithMessage(string.Format(Constants.ErrorMessages.INVALID_GUID, "ActivityTypeId"))
                .Must(activityTypeId => activityTypeId == null || _communityActivityTypeRepository.CheckCommunityActivityTypeExists(activityTypeId))
                .WithMessage(string.Format(Constants.ErrorMessages.NOT_FOUND, "CommunityActivityType"));
        }
    }
}
