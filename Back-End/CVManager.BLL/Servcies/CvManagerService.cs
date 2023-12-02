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
        public int CreateCv(CvDTO cv)
        {
           return _cvManagerRepository.CreateCv(cv);
        }

        public void DeleteCv(int cvId)
        {
            _cvManagerRepository.DeleteCv(cvId);
        }

        public CvDTO GetById(int cvId)
        {
          return _cvManagerRepository.GetById(cvId); 
        }

        public IPagedList<CvDTO> GetCvsList(int pageNumber, int pageSize)
        {
            return _cvManagerRepository.GetCvsList(pageNumber, pageSize);
        }

        public int UpdateCv(CvDTO cv)
        {
            return _cvManagerRepository.UpdateCv(cv);
        }

    }
}
