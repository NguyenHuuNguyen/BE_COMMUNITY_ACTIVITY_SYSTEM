using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Announcement;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Models;
using FluentValidation;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Ultis.FluentValidation
{
    public class AnnouncementUpdateDtoValidator : AbstractValidator<AnnouncementUpdateDto>
    {
        private readonly ICommonRepository _commonRepository;
        private readonly IAnnouncementRepository _announcementRepository;

        public AnnouncementUpdateDtoValidator(ICommonRepository commonRepository, IAnnouncementRepository announcementRepository)
        {
            _commonRepository = commonRepository;
            _announcementRepository = announcementRepository;

            RuleFor(x => x.Id)
                .Must(announcementId => announcementId == null || _commonRepository.IsGuid(announcementId))
                .WithMessage(string.Format(Constants.ErrorMessages.INVALID_GUID, "Id"))
                .Must(announcementId => announcementId == null || _announcementRepository.CheckAnnouncementExists(announcementId))
                .WithMessage(string.Format(Constants.ErrorMessages.NOT_FOUND, "Announcement"));
        }
    }
}
