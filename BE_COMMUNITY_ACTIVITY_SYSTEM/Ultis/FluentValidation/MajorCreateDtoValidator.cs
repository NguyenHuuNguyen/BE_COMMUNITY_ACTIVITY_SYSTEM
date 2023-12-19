﻿using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Major;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using FluentValidation;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Ultis.FluentValidation
{
    public class MajorCreateDtoValidator : AbstractValidator<MajorCreateDto>
    {
        private readonly ICommonRepository _commonRepository;
        private readonly IUserRepository _userRepository;

        public MajorCreateDtoValidator(ICommonRepository commonRepository, IUserRepository userRepository)
        {
            _commonRepository = commonRepository;
            _userRepository = userRepository;

            RuleFor(x => x.MajorHeadId)
                .Must(majorHeadId => majorHeadId == null || _commonRepository.IsGuid(majorHeadId))
                .WithMessage(string.Format(Constants.ErrorMessages.INVALID_GUID, "Id"))
                .Must(majorHeadId => majorHeadId == null || _userRepository.CheckUserExists(majorHeadId))
                .WithMessage(string.Format(Constants.ErrorMessages.NOT_FOUND, "User"))
                .Must(majorHeadId => majorHeadId == null || _userRepository.CheckUserIsStudent(majorHeadId) == false)
                .WithMessage(string.Format(Constants.ErrorMessages.INVALID_MAJORHEAD, "MajorHead"));
        }
    }
}
