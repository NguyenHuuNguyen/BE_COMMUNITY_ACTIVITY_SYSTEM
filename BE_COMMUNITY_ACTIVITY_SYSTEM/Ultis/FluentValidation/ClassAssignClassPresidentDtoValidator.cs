using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Class;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using FluentValidation;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Ultis.FluentValidation
{
    public class ClassAssignClassPresidentDtoValidator : AbstractValidator<ClassAssignClassPresidentDto>
    {
        private readonly ICommonRepository _commonRepository;
        private readonly IUserRepository _userRepository;
        private readonly IClassRepository _classRepository;

        public ClassAssignClassPresidentDtoValidator(ICommonRepository commonRepository, IUserRepository userRepository, IClassRepository classRepository)
        {
            _commonRepository = commonRepository;
            _userRepository = userRepository;
            _classRepository = classRepository;

            RuleFor(x => x.Id)
                .Must(classId => classId == null || _commonRepository.IsGuid(classId))
                .WithMessage(string.Format(Constants.ErrorMessages.INVALID_GUID, "Id"))
                .Must(classId => classId == null || _classRepository.CheckClassExists(classId))
                .WithMessage(string.Format(Constants.ErrorMessages.NOT_FOUND, "Class"));
            RuleFor(x => x.ClassPresidentId)
                .Must(classPresidentId => classPresidentId == null || _commonRepository.IsGuid(classPresidentId))
                .WithMessage(string.Format(Constants.ErrorMessages.INVALID_GUID, "Id"))
                .Must(classPresidentId => classPresidentId == null || _userRepository.CheckUserExists(classPresidentId))
                .WithMessage(string.Format(Constants.ErrorMessages.NOT_FOUND, "User"))
                .Must(classPresidentId => classPresidentId == null || _userRepository.CheckUserIsStudent(classPresidentId))
                .WithMessage(string.Format(Constants.ErrorMessages.INVALID_STUDENT, "ClassPresident"))
                .Must((dto, classPresidentId) => classPresidentId == null || _userRepository.CheckStudentBelongToClass(classPresidentId,dto.Id!))
                .WithMessage(string.Format(Constants.ErrorMessages.INVALID_CLASSPRESIDENT, "ClassPresident"));
        }
    }
}
