    using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Class;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using FluentValidation;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Ultis.FluentValidation
{
    public class ClassUpdateDtoValidator : AbstractValidator<ClassUpdateDto>
    {
        private readonly ICommonRepository _commonRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMajorRepository _majorRepository;
        private readonly IClassRepository _classRepository;

        public ClassUpdateDtoValidator(ICommonRepository commonRepository, IUserRepository userRepository, IMajorRepository majorRepository, IClassRepository classRepository)
        {
            _commonRepository = commonRepository;
            _userRepository = userRepository;
            _majorRepository = majorRepository;
            _classRepository = classRepository;

            RuleFor(x => x.Id)
                .Must(classId => classId == null || _commonRepository.IsGuid(classId))
                .WithMessage(string.Format(Constants.ErrorMessages.INVALID_GUID, "Id"))
                .Must(classId => classId == null || _classRepository.CheckClassExists(classId))
                .WithMessage(string.Format(Constants.ErrorMessages.NOT_FOUND, "Class"));
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
            RuleFor(x => x.ClassPresidentId)
                .Must(classPresidentId => classPresidentId == null || _commonRepository.IsGuid(classPresidentId))
                .WithMessage(string.Format(Constants.ErrorMessages.INVALID_GUID, "Id"))
                .Must(classPresidentId => classPresidentId == null || _userRepository.CheckUserExists(classPresidentId))
                .WithMessage(string.Format(Constants.ErrorMessages.NOT_FOUND, "User"))
                .Must(classPresidentId => classPresidentId == null || _userRepository.CheckUserIsStudent(classPresidentId))
                .WithMessage(string.Format(Constants.ErrorMessages.INVALID_STUDENT, "ClassPresident"))
                .Must((dto, classPresidentId) => classPresidentId == null || _userRepository.CheckStudentBelongToClass(classPresidentId, dto.Id!))
                .WithMessage(string.Format(Constants.ErrorMessages.INVALID_CLASSPRESIDENT, "ClassPresident"));
            RuleFor(x => x.Name)
                .Matches(Constants.Regexes.CLASSNAME)
                .WithMessage(string.Format(Constants.ErrorMessages.TEXT_NUMBER, "Name"));
            RuleFor(x => x.AcademicYear)
                .Must(academicYear => academicYear >= (int)Constants.Enums.MIN_ACADEMIC_YEAR)
                .WithMessage(Constants.ErrorMessages.INVALID_ACADEMIC_YEAR);
        }
    }
}
