using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.User;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using FluentValidation;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Ultis.FluentValidation
{
    public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
    {
        private readonly IClassRepository _classRepository;
        private readonly ICommonRepository _commonRepository;

        public UserCreateDtoValidator(IClassRepository classRepository, ICommonRepository commonRepository)
        {
            _classRepository = classRepository;
            _commonRepository = commonRepository;

            RuleFor(x => x.ClassId)
                .Must(classId => classId == null || _commonRepository.IsGuid(classId))
                .WithMessage(string.Format(Constants.ErrorMessages.INVALID_GUID, "ClassId"))
                .Must(classId => classId == null || _classRepository.CheckClassExists(classId))
                .WithMessage(string.Format(Constants.ErrorMessages.NOT_FOUND, "Class"))
                .Must((dto, classId) => dto.IsStudent || classId == null)
                .WithMessage(Constants.ErrorMessages.TEACHER_CAN_NOT_HAVE_CLASS);

            RuleFor(x => x.LastName)
                .Matches(Constants.Regexes.TEXT_ONLY)
                .WithMessage(string.Format(Constants.ErrorMessages.TEXT_ONLY, "Last Name"));

            RuleFor(x => x.FirstName)
                .Matches(Constants.Regexes.TEXT_ONLY)
                .WithMessage(string.Format(Constants.ErrorMessages.TEXT_ONLY, "First Name"));

            RuleFor(x => x.DateOfBirth)
                .Must( dob => dob < DateTime.Now)
                .WithMessage(string.Format(Constants.ErrorMessages.DATE_MUST_BE_EARLIER_THAN_CURRENT_TIME, "Date of Birth"));
        }
    }
}
