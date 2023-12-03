using CVManager.Core.DTOs;
using CVManager.Core.IRepositoreis;
using CVManager.Core.IServices;
using experienceInfoManager.Core.IRepositoreis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace CVManager.BLL.Servcies
{
    public class CvManagerService : ICvManagerService
    {
        private readonly ICvManagerRepository _cvManagerRepository;
        public CvManagerService(ICvManagerRepository cvManagerRepository)
        {
            _cvManagerRepository = cvManagerRepository;
        }
        public async Task<int> CreateCvAsync(CvDTO cv)
        {
           return await _cvManagerRepository.CreateCvAsync(cv);
        }

        public void DeleteCv(int cvId)
        {
            _cvManagerRepository.DeleteCv(cvId);
        }

        public async Task<CvDTO> GetByIdAsync(int cvId)
        {
          return await _cvManagerRepository.GetByIdAsync(cvId); 
        }

        public async Task<IPagedList<CvDTO>> GetCvsListAsync(int pageNumber, int pageSize)
        {
            return await _cvManagerRepository.GetCvsListAsync(pageNumber, pageSize);
        }

        public async Task<int> UpdateCvAsync(CvDTO cv)
        {
            return await _cvManagerRepository.UpdateCvAsync(cv);
        }

    }
}
