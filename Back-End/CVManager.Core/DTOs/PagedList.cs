using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace CVManager.Core.DTOs
{
    public class PagedList<T>
    {
        public IPagedList<T> Items { get; set; }
        public PagedListMetaData MetaData { get; set; }
    }
}
