using AutoMapper;
using AutoMapper.QueryableExtensions;
using CVManager.Core.DTOs;
using CVManager.Core.IRepositoreis;
using CVManager.DAL.DomainModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace CVManager.DAL.Repositories
{
    public class CvManagerRepository : ICvManagerRepository
    {
        private readonly IMapper _mapper;
        private readonly IConfigurationProvider _config;
        private readonly CVManagerDbContext _dbContext;
        public CvManagerRepository(IMapper mapper, CVManagerDbContext dbContext)
        {
             _mapper = mapper;
            _config = mapper.ConfigurationProvider;
            _dbContext = dbContext;
        }
        public int CreateCv(CvDTO cv)
        {

            var CvEntity = _mapper.Map<Cv>(cv);
            _dbContext.Add(CvEntity);
            _dbContext.SaveChanges();
            return CvEntity.Id;

        }

        public void DeleteCv(int cvId)
        {
           var cvEntity = _dbContext.Cvs
                            .Include(x => x.ExperienceInformation)
                            .Include(x => x.PersonalInformation)
                            .FirstOrDefault(cv => cv.Id == cvId);
            if(cvEntity != null)
            {
                _dbContext.Cvs.Remove(cvEntity);
                _dbContext.PersonalInformation.Remove(cvEntity.PersonalInformation);
                _dbContext.SaveChanges();
            }
        }

        public CvDTO? GetById(int cvId)
        {
            return _dbContext.Cvs
                            .Where(cv => cv.Id == cvId)
                            .ProjectTo<CvDTO>(_config)
                            .FirstOrDefault();
        }

        public IPagedList<CvDTO> GetCvsList(int pageNumber, int pageSize)
        {
           return _dbContext.Cvs.ProjectTo<CvDTO>(_config).ToPagedList(pageNumber, pageSize);
        }

        public int UpdateCv(CvDTO cv)
        {
            try
            {

                var CvEntity = _mapper.Map<Cv>(cv);
                _dbContext.Update(CvEntity);
                _dbContext.SaveChanges();
                return CvEntity.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
