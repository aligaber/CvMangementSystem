using CVManager.Core.DTOs;


namespace experienceInfoManager.Core.IRepositoreis
{
    public interface IExperienceInformationRepository
    {
        int Create(ExperienceInformationDTO experienceInfo);
        int Update(ExperienceInformationDTO experienceInfo);
        void Delete(int experienceInfoId);
    }

}