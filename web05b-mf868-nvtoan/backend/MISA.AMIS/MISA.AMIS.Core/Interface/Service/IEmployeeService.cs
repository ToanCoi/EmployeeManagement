using MISA.AMIS.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interface.Service
{
    public interface IEmployeeService : IBaseService<Employee>
    {
        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// CreatedBy: NVTOAN 06/07/2021
        string GetNewEmployeeCode();

        /// <summary>
        /// Hàm lấy bản ghi theo filter và chia paging
        /// </summary>
        /// <param name="filterValue">Giá trị filter</param>
        /// <param name="pageSize">Số bản ghi/trang</param>
        /// <param name="pageNum">Số thứ tự trang</param>
        /// <returns>Danh sách bản ghi theo filter cùng thông tin phân trang</returns>
        object GetEmployeesFilter(int? pageSize = null, int? pageNum = null, string filterValue = null);

        /// <summary>
        /// Export dữ liệu
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public MemoryStream Export(CancellationToken cancellationToken, string filterValue);

    }
}
