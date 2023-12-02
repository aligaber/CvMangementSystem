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
        IPagedList<CvDTO> GetCvsList(int pageNumber, int pageSize);
        CvDTO GetById(int cvId);
        int CreateCv(CvDTO cv);
        int UpdateCv(CvDTO cv);
        void DeleteCv(int cvId);
    }
}
