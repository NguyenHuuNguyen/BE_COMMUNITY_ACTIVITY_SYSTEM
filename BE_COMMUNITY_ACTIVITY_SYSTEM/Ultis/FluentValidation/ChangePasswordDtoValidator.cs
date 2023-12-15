using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Auth;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using FluentValidation;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Ultis.FluentValidation
{
    public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICommonRepository _commonRepository;

        public ChangePasswordDtoValidator(IUserRepository userRepository, ICommonRepository commonRepository)
        {
            _userRepository = userRepository;
            _commonRepository = commonRepository;

            RuleFor(x => x.UserId)
            .Must(userId => userId == null || _commonRepository.IsGuid(userId))
            .WithMessage(string.Format(Constants.ErrorMessages.INVALID_GUID, "UserId"))
            .Must(x => x == null || _userRepository.CheckUserExists(x))
            .WithMessage(Constants.ErrorMessages.USER_NOT_FOUND);

            RuleFor(x => x.Password)
                .Equal(x => x.PasswordConfirm)
                .WithMessage(Constants.ErrorMessages.PASSWORD_NOT_MATCHES);
        }
    }
}
