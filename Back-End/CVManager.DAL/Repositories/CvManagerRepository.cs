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
using System.Transactions;
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
        public async Task<int> CreateCvAsync(CvDTO cv)
        {

            var CvEntity = _mapper.Map<Cv>(cv);
            _dbContext.Add(CvEntity);
            await _dbContext.SaveChangesAsync();

            await AddExperiencesAsync(cv.ExperienceInformation);
            return CvEntity.Id;

        }

        public async void DeleteCv(int cvId)
        {
           var cvEntity = _dbContext.Cvs
                            .Include(x => x.ExperienceInformation)
                            .Include(x => x.PersonalInformation)
                            .FirstOrDefault(cv => cv.Id == cvId);
            if(cvEntity != null)
            {
                _dbContext.Cvs.Remove(cvEntity);
                _dbContext.PersonalInformation.Remove(cvEntity.PersonalInformation);
               await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<CvDTO?> GetByIdAsync(int cvId)
        {
            return await _dbContext.Cvs
                            .Where(cv => cv.Id == cvId)
                            .ProjectTo<CvDTO>(_config)
                            .FirstOrDefaultAsync();
        }

        public async Task<IPagedList<CvDTO>> GetCvsListAsync(int pageNumber, int pageSize)
        {
           return await _dbContext.Cvs.ProjectTo<CvDTO>(_config).ToPagedListAsync(pageNumber, pageSize);
        }

        public async Task<int> UpdateCvAsync(CvDTO cv)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var CvEntity = _mapper.Map<Cv>(cv);
                    _dbContext.Update(CvEntity);
                   await _dbContext.SaveChangesAsync();
                   
                    await RemoveCvExperiencesAsync(cv.Id);
                    await AddExperiencesAsync(cv.ExperienceInformation);

                    scope.Complete();
                    return CvEntity.Id;
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception(ex.Message, ex);
                } 
            }
        }


        //this section my transfered to new ExperienceRepository
        private async Task AddExperiencesAsync(ICollection<ExperienceInformationDTO> experiences)
        {
            var newExperiences = _mapper.Map<List<ExperienceInformation>>(experiences);
            if (newExperiences.Any())
            {
                newExperiences.ForEach(x => x.Id = 0);
                _dbContext.AddRange(newExperiences);
                await _dbContext.SaveChangesAsync();
            }
        }

        private async Task RemoveCvExperiencesAsync(int cvId)
        {
            var experiences = _dbContext.ExperienceInformation.Where(x => x.CvId == cvId).ToList();
            if (experiences.Any())
            {
                _dbContext.ExperienceInformation.RemoveRange(experiences);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
