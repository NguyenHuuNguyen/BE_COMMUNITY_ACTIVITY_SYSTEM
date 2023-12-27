using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Class;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using FluentValidation;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Ultis.FluentValidation
{
    public class ClassCreateDtoValidator : AbstractValidator<ClassCreateDto>
    {
        private readonly ICommonRepository _commonRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMajorRepository _majorRepository;

        public ClassCreateDtoValidator(ICommonRepository commonRepository, IUserRepository userRepository, IMajorRepository majorRepository)
        {
            _commonRepository = commonRepository;
            _userRepository = userRepository;
            _majorRepository = majorRepository;

            RuleFor(x => x.MajorId)
                .Must(majorId => majorId == null || _commonRepository.IsGuid(majorId))
                .WithMessage(string.Format(Constants.ErrorMessages.INVALID_GUID, "Id"))
                .Must(majorId => majorId == null || _majorRepository.CheckMajorExist(majorId))
                .WithMessage(string.Format(Constants.ErrorMessages.NOT_FOUND, "Major"));
            RuleFor(x => x.HeadTeacherId)
                .Must(headTeacherId => headTeacherId == null || _commonRepository.IsGuid(headTeacherId))
                .WithMessage(string.Format(Constants.ErrorMessages.INVALID_GUID, "Id"))
                .Must(headTeacherId => headTeacherId == null || _userRepository.CheckUserExists(headTeacherId))
                .WithMessage(string.Format(Constants.ErrorMessages.NOT_FOUND, "User"))
                .Must(headTeacherId => headTeacherId == null || _userRepository.CheckUserIsStudent(headTeacherId) == false)
                .WithMessage(string.Format(Constants.ErrorMessages.INVALID_HEAD, "HeadTeacher"));
            RuleFor(x => x.Name)
                .Matches(Constants.Regexes.CLASSNAME)
                .WithMessage(string.Format(Constants.ErrorMessages.TEXT_NUMBER, "Name"));
            RuleFor(x => x.AcademicYear)
                .Must(academicYear => academicYear >= (int)Constants.Enums.MIN_ACADEMIC_YEAR)
                .WithMessage(Constants.ErrorMessages.INVALID_ACADEMIC_YEAR);
        }
    }
}
