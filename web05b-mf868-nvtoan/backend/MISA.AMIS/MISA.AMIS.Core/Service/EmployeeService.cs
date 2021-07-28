using ClosedXML.Excel;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Enum;
using MISA.AMIS.Core.Interface.Repository;
using MISA.AMIS.Core.Interface.Service;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Service
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        #region Declare
        IEmployeeRepository _employeeRepository;
        #endregion

        #region Constructor
        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        #endregion

        #region Method
        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// CreatedBy: NVTOAN 06/07/2021
        public string GetNewEmployeeCode()
        {
            var maxEmployeeCode = _employeeRepository.GetMaxEmployeeCode();

            var index = maxEmployeeCode.IndexOfAny(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });

            //Lấy ra các phần số và không phải số
            var alphaPart = maxEmployeeCode.Substring(0, index);
            var numberPart = maxEmployeeCode.Substring(index);
            var numberLength = numberPart.Length;

            //Tăng mã lên 1
            numberPart = (Int32.Parse(numberPart) + 1).ToString();

            //Thêm 0 vào đầu nếu thiếu
            numberPart = numberPart.PadLeft(numberLength, '0');

            var nextCode = alphaPart + numberPart;

            return nextCode;
        }

        /// <summary>
        /// Hàm lấy bản ghi theo filter và chia paging
        /// </summary>
        /// <param name="filterValue">Giá trị filter</param>
        /// <param name="pageSize">Số bản ghi/trang</param>
        /// <param name="pageNum">Số thứ tự trang</param>
        /// <returns>Danh sách bản ghi theo filter cùng thông tin phân trang</returns>
        public object GetEmployeesFilter(int? pageSize = null, int? pageNum = null, string filterValue = null)
        {
            //Lấy dữ liệu về
            var employees = _employeeRepository.GetEmployeesFilter(pageSize, pageNum, filterValue);

            //Lấy tổng số bản ghi
            var totalRecord = Int32.Parse(_employeeRepository.GetTotalRecord(filterValue));

            //Dữ liệu trả về
            var data = new
            {
                TotalRecord = totalRecord,
                TotalPage = pageSize != null ? Math.Ceiling((decimal)((decimal)totalRecord / pageSize)) : 1,
                data = employees
            };

            return data;
        }

        /// <summary>
        /// Export dữ liệu
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Memmory stream</returns>
        public MemoryStream Export(CancellationToken cancellationToken, string filterValue)
        {
            //Lấy ra danh sách nhân viên
            List<Employee> employees = _employeeRepository.GetEmployeesFilter(filterValue: filterValue).ToList();

            //Lấy ra danh sách cột cần xuất khẩu
            List<EmployeeExportColumn> exportColumns = _employeeRepository.GetEmployeeExportColumns().ToList();

            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                //Chỉnh header của excel
                worksheet.Cells[3, 1].Value = "STT";
                worksheet.Cells[3, 1].Style.Font.Bold = true;
                worksheet.Cells[3, 1].Style.Fill.SetBackground(Color.LightGray);
                worksheet.Cells[3, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[3, 1].Style.Border.Top.Color.SetColor(Color.Black);
                worksheet.Cells[3, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[3, 1].Style.Border.Left.Color.SetColor(Color.Black);
                worksheet.Cells[3, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[3, 1].Style.Border.Right.Color.SetColor(Color.Black);
                worksheet.Cells[3, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[3, 1].Style.Border.Bottom.Color.SetColor(Color.Black);

                for (int i = 1; i <= exportColumns.Count; i++)
                {
                    if(exportColumns[i - 1].FieldName == "DateOfBirth")
                    {
                        worksheet.Column(i + 1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }   
                    worksheet.Column(i + 1).Width = exportColumns[i - 1].Width;
                    worksheet.Cells[3, i + 1].Value = exportColumns[i - 1].DisplayName;
                    worksheet.Cells[3, i + 1].Style.Font.Bold = true;
                    worksheet.Cells[3, i + 1].Style.Fill.SetBackground(Color.LightGray);
                    worksheet.Cells[3, i + 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[3, i + 1].Style.Border.Top.Color.SetColor(Color.Black);
                    worksheet.Cells[3, i + 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[3, i + 1].Style.Border.Left.Color.SetColor(Color.Black);
                    worksheet.Cells[3, i + 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[3, i + 1].Style.Border.Right.Color.SetColor(Color.Black);
                    worksheet.Cells[3, i + 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[3, i + 1].Style.Border.Bottom.Color.SetColor(Color.Black);
                    worksheet.Column(i + 1).Width = exportColumns[i - 1].Width;
                }
                for (int index = 1; index <= employees.Count; index++)
                {
                    //Màu header
                    worksheet.Cells[index + 3, 1].Value = index;
                    worksheet.Cells[index + 3, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[index + 3, 1].Style.Border.Top.Color.SetColor(Color.Black);
                    worksheet.Cells[index + 3, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[index + 3, 1].Style.Border.Left.Color.SetColor(Color.Black);
                    worksheet.Cells[index + 3, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[index + 3, 1].Style.Border.Right.Color.SetColor(Color.Black);
                    worksheet.Cells[index + 3, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[index + 3, 1].Style.Border.Bottom.Color.SetColor(Color.Black);
                    for (int i = 1; i <= exportColumns.Count; i++)
                    {
                        //Màu border
                        worksheet.Cells[index + 3, i + 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[index + 3, i + 1].Style.Border.Top.Color.SetColor(Color.Black);
                        worksheet.Cells[index + 3, i + 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[index + 3, i + 1].Style.Border.Left.Color.SetColor(Color.Black);
                        worksheet.Cells[index + 3, i + 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[index + 3, i + 1].Style.Border.Right.Color.SetColor(Color.Black);
                        worksheet.Cells[index + 3, i + 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[index + 3, i + 1].Style.Border.Bottom.Color.SetColor(Color.Black);
                        //Xử lý trường hợp là giới tính
                        if (exportColumns[i - 1].FieldName == "Gender")
                        {
                            if (GetValueByProperty(employees[index - 1], exportColumns[i - 1].FieldName) is null)
                            {
                                worksheet.Cells[index + 3, i + 1].Value = "";
                                continue;
                            }
                            switch (int.Parse(GetValueByProperty(employees[index - 1], exportColumns[i - 1].FieldName).ToString()))
                            {
                                case 0:
                                    worksheet.Cells[index + 3, i + 1].Value = "Nam";
                                    break;
                                case 1:
                                    worksheet.Cells[index + 3, i + 1].Value = "Nữ";
                                    break;
                                case 2:
                                    worksheet.Cells[index + 3, i + 1].Value = "Khác";
                                    break;
                                default:
                                    worksheet.Cells[index + 3, i + 1].Value = "";
                                    break;
                            }
                            continue;
                        }
                        worksheet.Cells[index + 3, i + 1].Value = GetValueByProperty(employees[index - 1], exportColumns[i - 1].FieldName);
                    }
                }
                worksheet.Cells["A1:K1"].Merge = true;
                worksheet.Cells[1, 1].Value = "DANH SÁCH NHÂN VIÊN";
                worksheet.Cells[1, 1].Style.Font.Size = 16;
                worksheet.Cells[1, 1].Style.Font.Bold = true;
                worksheet.Cells[1, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["A2:K2"].Merge = true;

                package.Save();
            }
            stream.Position = 0;
            return stream;
        }

        private object GetValueByProperty(Employee employee, string propName)
        {
            var propertyInfo = employee.GetType().GetProperty(propName);
            if (propertyInfo.PropertyType == typeof(DateTime) || propertyInfo.PropertyType == typeof(DateTime?))
            {
                var value = employee.GetType().GetProperty(propName).GetValue(employee, null);
                var date = Convert.ToDateTime(value).ToString("dd/MM/yyyy");

                return value != null ? date : "";
            }

            return employee.GetType().GetProperty(propName).GetValue(employee, null);
        }
        #endregion
    }
}
