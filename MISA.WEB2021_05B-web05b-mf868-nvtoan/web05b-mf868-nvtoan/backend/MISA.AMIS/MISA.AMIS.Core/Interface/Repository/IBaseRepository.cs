using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interface.Repository
{
    public interface IBaseRepository<TEntity>
    {
        /// <summary>
        /// Lấy toàn bộ bản ghi
        /// </summary>
        /// <returns>List bản ghi lấy được</returns>
        /// CreatedBy: NVTOAN 06/07/2021
        IEnumerable<TEntity> GetEntities();

        /// <summary>
        /// Lấy bản ghi theo Id
        /// </summary>
        /// <param name="Id">Id của đối tượng cần lấy</param>
        /// <returns>Một bản ghi lấy được theo Id</returns>
        /// CreatedBy: NVTOAN 06/07/2021
        TEntity GetEntityById(Guid Id);

        /// <summary>
        /// Lấy bản ghi theo property - dùng cho backend validate theo FormMode
        /// </summary>
        /// <param name="entity">Object cần kiểm tra</param>
        /// <param name="propName">Tên của trường cần kiểm tra</param>
        /// <returns>Một bản ghi có property với value truyền vào</returns>
        /// CreatedBy: NVTOAN 06/07/2021
        TEntity GetEntityByProperty(TEntity entity, string propName);

        /// <summary>
        /// Hàm lấy bản ghi theo property - cho frontend dùng
        /// </summary>
        /// <param name="propName">Tên trường cần kiểm tra</param>
        /// <param name="value">Giá trị của thuộc tính</param>
        /// <returns>Một bản ghi có property và value truyền vào</returns>
        /// CreatedBy: NVTOAN 09/07/2021
        TEntity GetEntityByProperty(string propName, string propValue);


        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm mới</param>
        /// <returns>Số dòng bị ảnh hưởng</returns>
        /// CreatedBy: NVTOAN 06/07/2021
        int InsertEntity(TEntity entity);

        /// <summary>
        /// Sửa thông tin một bản ghi
        /// </summary>
        /// <param name="Id">Id của bản ghi cần sửa</param>
        /// <param name="entity">Đối tượng có những thông tin cần sửa</param>
        /// <returns>Số dòng bị ảnh hưởng</returns>
        /// CreatedBy: NVTOAN 06/07/2021
        int UpdateEntity(Guid Id, TEntity entity);

        /// <summary>
        /// Xóa một bản ghi theo Id
        /// </summary>
        /// <param name="Id">Id của bản ghi cần xóa</param>
        /// <returns>Số dòng bị ảnh hưởng</returns>
        /// CreatedBy: NVTOAN 06/07/2021
        int DeleteEntity(Guid Id);

        /// <summary>
        /// Xóa nhiều bản ghi theo Id
        /// </summary>
        /// <param name="listId">Danh sách id bản ghi cần xóa</param>
        /// <returns>Số bản ghi xóa được</returns>
        int MultipleDelete(IEnumerable<Guid> listId);
    }
}
