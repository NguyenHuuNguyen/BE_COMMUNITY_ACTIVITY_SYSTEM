using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.User;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using FluentValidation;
using System;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Ultis.FluentValidation
{
    public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
    {
        private readonly IClassRepository _classRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICommonRepository _commonRepository;

        public UserUpdateDtoValidator(IClassRepository classRepository, IUserRepository userRepository, ICommonRepository commonRepository)
        {
            _classRepository = classRepository;
            _userRepository = userRepository;
            _commonRepository = commonRepository;

            RuleFor(x => x.Id)
                .Must(userId => userId == null || _commonRepository.IsGuid(userId))
                .WithMessage(string.Format(Constants.ErrorMessages.INVALID_GUID, "Id"))
                .Must(userId => userId == null || _userRepository.CheckUserExists(userId))
                .WithMessage(string.Format(Constants.ErrorMessages.NOT_FOUND, "User"));

            RuleFor(x => x.ClassId)
                .Must(classId => classId == null || _commonRepository.IsGuid(classId))
                .WithMessage(string.Format(Constants.ErrorMessages.INVALID_GUID, "ClassId"))
                .Must(classId => classId == null || _classRepository.CheckClassExists(classId))
                .WithMessage(string.Format(Constants.ErrorMessages.NOT_FOUND, "Class"))
                .Must((dto, classId) => (dto.Id != null && userRepository.CheckUserIsStudent(dto.Id)) || classId == null)
                .WithMessage(Constants.ErrorMessages.TEACHER_CAN_NOT_HAVE_CLASS);

            RuleFor(x => x.LastName)
                .Matches(Constants.Regexes.TEXT_ONLY)
                .WithMessage(string.Format(Constants.ErrorMessages.TEXT_ONLY, "Last Name"));

            RuleFor(x => x.FirstName)
                .Matches(Constants.Regexes.TEXT_ONLY)
                .WithMessage(string.Format(Constants.ErrorMessages.TEXT_ONLY, "First Name"));

            RuleFor(x => x.DateOfBirth)
                .Must(IsValidDate)
                .WithMessage(string.Format(Constants.ErrorMessages.DATE_MUST_BE_EARLIER_THAN_CURRENT_TIME, "Date of Birth"));

            RuleFor(x => x.Ethnic)
                .Matches(Constants.Regexes.TEXT_ONLY)
                .WithMessage(string.Format(Constants.ErrorMessages.TEXT_ONLY, "Ethnic"));

            RuleFor(x => x.Nationality)
                .Matches(Constants.Regexes.TEXT_ONLY)
                .WithMessage(string.Format(Constants.ErrorMessages.TEXT_ONLY, "Nationality"));

            RuleFor(x => x.IdentificationCardIssueDate)
                .Must(IsValidDate)
                .WithMessage(string.Format(Constants.ErrorMessages.DATE_MUST_BE_EARLIER_THAN_CURRENT_TIME, "Identification Card Issue Date"));

            RuleFor(x => x.Religion)
                .Matches(Constants.Regexes.TEXT_ONLY)
                .WithMessage(string.Format(Constants.ErrorMessages.TEXT_ONLY, "Religion"));

            RuleFor(x => x.Phone)
                .Matches(Constants.Regexes.PHONE_NUMBER)
                .WithMessage(string.Format(Constants.ErrorMessages.INVALID_FIELD, "Phone number"));

            RuleFor(x => x.Email)
                .Matches(Constants.Regexes.EMAIL)
                .WithMessage(string.Format(Constants.ErrorMessages.INVALID_FIELD, "Email"));

            RuleFor(x => x.Facebook)
                .Matches(Constants.Regexes.WEBSITE_LINK)
                .WithMessage(string.Format(Constants.ErrorMessages.INVALID_FIELD, "Facebook"));

            RuleFor(x => x.City)
                .Matches(Constants.Regexes.TEXT_ONLY)
                .WithMessage(string.Format(Constants.ErrorMessages.TEXT_ONLY, "City"));

            RuleFor(x => x.District)
                .Matches(Constants.Regexes.TEXT_ONLY)
                .WithMessage(string.Format(Constants.ErrorMessages.TEXT_ONLY, "District"));

            RuleFor(x => x.Ward)
                .Matches(Constants.Regexes.TEXT_ONLY)
                .WithMessage(string.Format(Constants.ErrorMessages.TEXT_ONLY, "Ward"));
        }

        private bool IsValidDate(DateTime date)
        {
            return date < DateTime.Now;
        }
    }
}
