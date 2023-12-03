using CVManager.Core.DTOs;
using System;
using System.Threading.Tasks;
using X.PagedList;

namespace CVManager.Core.IRepositoreis
{
    public interface ICvManagerRepository
    {
        Task<IPagedList<CvDTO>> GetCvsListAsync(int pageNumber, int pageSize);
        Task<CvDTO> GetByIdAsync(int cvId);
        Task<int> CreateCvAsync(CvDTO cv);
        Task<int> UpdateCvAsync(CvDTO cv);
        void DeleteCv(int cvId);
    }

}