using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.CommunityActivity;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using FluentValidation;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Ultis.FluentValidation
{
    public class CommunityActivityUpdateDtoValidator : AbstractValidator<CommunityActivityUpdateDto>
    {
        private readonly ICommonRepository _commonRepository;
        private readonly ICommunityActivityRepository _communityActivityRepository;
        private readonly ICommunityActivityTypeRepository _communityActivityTypeRepository;

        public CommunityActivityUpdateDtoValidator(ICommonRepository commonRepository, ICommunityActivityRepository communityActivityRepository, ICommunityActivityTypeRepository communityActivityTypeRepository)
        {
            _commonRepository = commonRepository;
            _communityActivityRepository = communityActivityRepository;
            _communityActivityTypeRepository = communityActivityTypeRepository;

            RuleFor(x => x.Id)
                .Must(caId => caId == null || _commonRepository.IsGuid(caId))
                .WithMessage(string.Format(Constants.ErrorMessages.INVALID_GUID, "Id"))
                .Must(caId => caId == null || _communityActivityRepository.CheckCommunityActivityExist(caId))
                .WithMessage(string.Format(Constants.ErrorMessages.NOT_FOUND, "CommunityActivity"));

            RuleFor(x => x.ActivityTypeId)
                .Must(activityTypeId => activityTypeId == null || _commonRepository.IsGuid(activityTypeId))
                .WithMessage(string.Format(Constants.ErrorMessages.INVALID_GUID, "ActivityTypeId"))
                .Must(activityTypeId => activityTypeId == null || _communityActivityTypeRepository.CheckCommunityActivityTypeExists(activityTypeId))
                .WithMessage(string.Format(Constants.ErrorMessages.NOT_FOUND, "CommunityActivityType"));
        }
    }
}
