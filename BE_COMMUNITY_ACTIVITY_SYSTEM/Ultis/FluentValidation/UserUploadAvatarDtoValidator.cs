using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.User;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using FluentValidation;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Ultis.FluentValidation
{
    public class UserUploadAvatarDtoValidator : AbstractValidator<UserUploadAvatarDto>
    {
        private readonly ICommonRepository _commonRepository;
        private readonly IUserRepository _userRepository;

        public UserUploadAvatarDtoValidator(ICommonRepository commonRepository, IUserRepository userRepository)
        {
            _commonRepository = commonRepository;
            _userRepository = userRepository;

            RuleFor(x => x.UserId)
                .Must(userId => userId == null || _commonRepository.IsGuid(userId))
                .WithMessage(string.Format(Constants.ErrorMessages.INVALID_GUID, "Id"))
                .Must(userId => userId == null || _userRepository.CheckUserExists(userId))
                .WithMessage(string.Format(Constants.ErrorMessages.NOT_FOUND, "User"));
        }
    }
}
