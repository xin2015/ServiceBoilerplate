using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBoilerplate.DAL.Models
{
    /// <summary>
    /// 分页
    /// </summary>
    public class Paging
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int Pagination { get; set; }
        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总数量
        /// </summary>
        public int Count { get; set; }

        public Paging(int pagination, int pageSize)
        {
            Pagination = pagination;
            PageSize = pageSize;
        }
    }
}
