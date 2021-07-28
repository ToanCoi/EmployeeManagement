using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MISA.AMIS.API.Controllers
{
    public class EmployeesController : BaseController<Employee>
    {
        #region Declare
        IEmployeeService _employeeService;
        #endregion

        #region Constructor
        public EmployeesController(IEmployeeService employeeService) : base(employeeService)
        {
            _employeeService = employeeService;
        }
        #endregion

        #region Method
        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// CreatedBy: NVTOAN 11/07/2021
        [EnableCors("AllowCROSPolicy")]
        [HttpGet("NewEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            var newEmployeeCode = _employeeService.GetNewEmployeeCode();

            if (newEmployeeCode != null)
            {
                return Ok(newEmployeeCode);
            }
            else
            {
                return NoContent();
            }
        }


        /// <summary>
        /// Lấy dữ liệu filter theo mã, tên, số điện thoại
        /// </summary>
        /// <param name="pageSize">Số bản ghi/trang</param>
        /// <param name="pageNum">Số trang hiện tại</param>
        /// <param name="filterValue">Giá trị filter</param>
        /// <returns>Dữ liệu gồm bản ghi theo filter, tổng số trang</returns>
        /// CreatedBy: NVTOAN 11/07/2021
        [EnableCors("AllowCROSPolicy")]
        [HttpGet("Filter")]
        public IActionResult GetEmployeesFilter([FromQuery]int? pageSize, [FromQuery]int? pageNum, [FromQuery] string filterValue)
        {
            var employees = _employeeService.GetEmployeesFilter(pageSize, pageNum, filterValue);
            
            if(employees != null)
            {
                return Ok(employees);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Hàm export dữ liệu sang excel       
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="filterValue">Giá trị filter</param>
        /// <returns></returns>
        [EnableCors("AllowCROSPolicy")]
        [HttpGet("Export")]
        public IActionResult Export(CancellationToken cancellationToken, [FromQuery] string filterValue)
        {
            var stream = _employeeService.Export(cancellationToken, filterValue);

            string excelName = $"UserList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
        #endregion
    }
}
