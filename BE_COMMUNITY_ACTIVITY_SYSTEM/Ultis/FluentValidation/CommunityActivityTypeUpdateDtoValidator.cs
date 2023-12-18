﻿using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.CommunityActivityType;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using FluentValidation;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Ultis.FluentValidation
{
    public class CommunityActivityTypeUpdateDtoValidator : AbstractValidator<CommunityActivityTypeUpdateDto>
    {
        private readonly ICommonRepository _commonRepository;
        private readonly ICommunityActivityTypeRepository _communityActivityTypeRepository;

        public CommunityActivityTypeUpdateDtoValidator(ICommonRepository commonRepository, ICommunityActivityTypeRepository communityActivityTypeRepository)
        {
            _commonRepository = commonRepository;
            _communityActivityTypeRepository = communityActivityTypeRepository;

            RuleFor(x => x.Id)
                .Must(communityActivityTypeId => communityActivityTypeId == null || _commonRepository.IsGuid(communityActivityTypeId))
                .WithMessage(string.Format(Constants.ErrorMessages.INVALID_GUID, "Id"))
                .Must(communityActivityTypeId => communityActivityTypeId == null || _communityActivityTypeRepository.CheckCommunityActivityTypeExists(communityActivityTypeId))
                .WithMessage(string.Format(Constants.ErrorMessages.NOT_FOUND, "CommunityActivityType"));
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