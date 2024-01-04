using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Class;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using FluentValidation;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Ultis.FluentValidation
{
    public class ClassPaginationRequestDtoValidator : AbstractValidator<ClassPaginationRequestDto>
    {
        private readonly ICommonRepository _commonRepository;

        public ClassPaginationRequestDtoValidator(ICommonRepository commonRepository)
        {
            _commonRepository = commonRepository;

            RuleFor(x => x.MajorId)
                .Must(majorId => majorId == null || majorId == string.Empty || _commonRepository.IsGuid(majorId))
                .WithMessage(string.Format(Constants.ErrorMessages.INVALID_GUID, "MajorId"));
        }
    }
}
