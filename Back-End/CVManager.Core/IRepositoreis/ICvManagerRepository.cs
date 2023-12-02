using CVManager.Core.DTOs;
using System;
using X.PagedList;

namespace CVManager.Core.IRepositoreis
{
    public interface ICvManagerRepository
    {
        IPagedList<CvDTO> GetCvsList(int pageNumber, int pageSize);
        CvDTO GetById(int cvId);
        int CreateCv(CvDTO cv);
        int UpdateCv(CvDTO cv);
        void DeleteCv(int cvId);
    }

}