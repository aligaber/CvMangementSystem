using CVManager.Core.DTOs;
using personalInfoManager.Core.DTOs;
using System;
using X.PagedList;

namespace CVManager.Core.IRepositoreis
{
    public interface IPersonalInformationRepository
    {
        int Create(PersonalInformationDTO personalInformation);
        int Update(PersonalInformationDTO personalInformation);
        void Delete(int id);
    }

}