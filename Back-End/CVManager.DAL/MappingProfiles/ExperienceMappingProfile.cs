using AutoMapper;
using CVManager.Core.DTOs;
using CVManager.DAL.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVManager.DAL.MappingProfiles
{
    public class ExperienceMappingProfile : Profile
    {
        public ExperienceMappingProfile()
        {
            CreateMap<ExperienceInformation, ExperienceInformationDTO>().ReverseMap();
        }
    }
}
