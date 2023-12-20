using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Major;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using FluentValidation;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Ultis.FluentValidation
{
    public class MajorUpdateDtoValidator : AbstractValidator<MajorUpdateDto>
    {
        private readonly ICommonRepository _commonRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMajorRepository _majorRepository;

        public MajorUpdateDtoValidator(ICommonRepository commonRepository, IUserRepository userRepository, IMajorRepository majorRepository)
        {
            _commonRepository = commonRepository;
            _userRepository = userRepository;
            _majorRepository = majorRepository;

            RuleFor(x => x.Id)
                .Must(majorId => majorId == null || _commonRepository.IsGuid(majorId))
                .WithMessage(string.Format(Constants.ErrorMessages.INVALID_GUID, "Id"))
                .Must(majorId => majorId == null || _majorRepository.CheckMajorExist(majorId))
                .WithMessage(string.Format(Constants.ErrorMessages.NOT_FOUND, "Major"));
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
