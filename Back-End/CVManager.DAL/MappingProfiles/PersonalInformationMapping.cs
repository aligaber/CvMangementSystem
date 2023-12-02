using AutoMapper;
using CVManager.Core.DTOs;
using CVManager.DAL.DomainModels;
using personalInfoManager.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVManager.DAL.MappingProfiles
{
    public class PersonalInformationMapping : Profile
    {
        public PersonalInformationMapping()
        {
            CreateMap<PersonalInformation, PersonalInformationDTO>().ReverseMap();
        }
    }
}
