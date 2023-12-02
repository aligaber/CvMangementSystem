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
    public class CvManagerProfile : Profile
    {
        public CvManagerProfile()
        {
            CreateMap<Cv, CvDTO>().ReverseMap();
        }
    }
}
