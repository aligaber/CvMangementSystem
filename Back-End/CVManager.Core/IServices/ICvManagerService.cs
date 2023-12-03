using CVManager.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace CVManager.Core.IServices
{
    public interface ICvManagerService
    {
       Task<IPagedList<CvDTO>> GetCvsListAsync(int pageNumber, int pageSize);
        Task<CvDTO> GetByIdAsync(int cvId);
        Task<int> CreateCvAsync(CvDTO cv);
        Task<int> UpdateCvAsync(CvDTO cv);
        void DeleteCv(int cvId);
    }
}
