using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Auth;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using FluentValidation;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Ultis.FluentValidation
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.AccountId)
                .Matches(Constants.Regexes.ACCOUNT_ID)
                .WithMessage(string.Format(Constants.ErrorMessages.TEXT_AND_NUMBER_ONLY, "AccountId"));
        }
    }
}
