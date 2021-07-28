using MISA.AMIS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interface.Repository
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        /// <summary>
        /// Lấy mã nhân viên lớn nhất trong db
        /// </summary>
        /// <returns>Mã nhân lớn nhất</returns>
        /// CreatedBy: NVTOAN 06/07/2021
        string GetMaxEmployeeCode();

        /// <summary>
        /// Lấy danh sách nhân viên theo mã hoặc tên
        /// </summary>
        /// <param name="filterValue">Giá trị filter</param>
        /// <param name="pageSize">Số bản ghi/trang</param>
        /// <param name="pageNum">Số trang</param>
        /// <returns>Danh sách nhân viên theo giá trị truyền vào</returns>
        /// CreatedBy: NVTOAN 06/07/2021
        IEnumerable<Employee> GetEmployeesFilter(int? pageSize = null, int? pageNum = null, string filterValue = null);

        /// <summary>
        /// Lấy tổng số bản ghi theo filter
        /// </summary>
        /// <returns>Giá trị filter</returns>
        /// CreatedBy: NVTOAN 06/07/2021
        string GetTotalRecord(string filterValue);

        /// <summary>
        /// Lấy bảng để mapping cột với dữ liệu khi export
        /// </summary>
        /// <returns>Thông tin các cột export</returns>
        /// CreatedBy: NVTOAN 12/07/2021
        public IEnumerable<EmployeeExportColumn> GetEmployeeExportColumns();
    }
}
