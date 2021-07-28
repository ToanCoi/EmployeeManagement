using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Enum;
using MISA.AMIS.Core.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MISA.AMIS.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<TEntity> : ControllerBase
    {
        #region Declare
        IBaseService<TEntity> _baseService;
        #endregion

        #region Constructor
        public BaseController(IBaseService<TEntity> baseService)
        {
            _baseService = baseService;
        }
        #endregion

        #region Method
        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// CreatedBy: NVTOAN 24/06/2021
        [EnableCors("AllowCROSPolicy")]
        [HttpGet]
        public IActionResult Get()
        {
            var entities = _baseService.GetEntities();

            if (entities != null)
            {
                return Ok(entities);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Lấy bản ghi theo Id
        /// </summary>
        /// <param name="Id">Id của bản ghi</param>
        /// <returns>Một đối tượng tìm được theo Id</returns>
        /// CreateedBy: NVTOAN 24/06/2021
        [EnableCors("AllowCROSPolicy")]
        [HttpGet("{Id}")]
        public IActionResult Get([FromRoute] Guid Id)
        {
            var entity = _baseService.GetEntityById(Id);

            if (entity != null)
            {
                return Ok(entity);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Lấy bản ghi theo property
        /// </summary>
        /// <param name="propName">Tên thuộc tính của entity cần lấy</param>
        /// <param name="propValue">Giá trị thuộc tính</param>
        /// <returns>Một bản ghi lấy được có property và value truyền vào</returns>
        /// CreatedBy: NVTOAN 09/07/2021
        [EnableCors("AllowCROSPolicy")]
        [HttpGet("Property")]
        public IActionResult GetByProperty([FromQuery] string propName, [FromQuery] string propValue)
        {
            var entity = _baseService.GetEntityByProperty(propName, propValue);

            if(entity != null)
            {
                return Ok(entity);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm</param>
        /// <returns>Số dòng bị ảnh hưởng</returns>
        [EnableCors("AllowCROSPolicy")]
        [HttpPost]
        public IActionResult Post([FromBody] TEntity entity)
        {
            var serviceResult = _baseService.InsertEntity(entity);

            switch(serviceResult.Code)
            {
                case MISACode.Exception:
                    return StatusCode(500, serviceResult);
                case MISACode.InvalidData:
                    return BadRequest(serviceResult);
                default:
                    return StatusCode(201, serviceResult);
            }
        }

        /// <summary>
        /// Sửa thông tin một bản ghi
        /// </summary>
        /// <param name="Id">Id của bản ghi cần sửa</param>
        /// <param name="entity">Đối tượng với thông tin cần sửa</param>
        /// <returns>Số dòng bị ảnh hưởng</returns>
        [EnableCors("AllowCROSPolicy")]
        [HttpPut("{Id}")]
        public IActionResult Put(Guid Id, TEntity entity)
        {
            var serviceResult = _baseService.UpdateEntity(Id, entity);

            switch (serviceResult.Code)
            {
                case MISACode.Exception:
                    return StatusCode(500, serviceResult);
                case MISACode.InvalidData:
                    return BadRequest(serviceResult);
                default:
                    return Ok(serviceResult);
            }
        }

        /// <summary>
        /// Xóa một bản ghi theo Id
        /// </summary>
        /// <param name="Id">Id của bản ghi cần xóa</param>
        /// <returns>Số dòng bị ảnh hưởng</returns>
        [EnableCors("AllowCROSPolicy")]
        [HttpDelete("{Id}")]
        public IActionResult Delete(Guid Id)
        {
            var serviceResult = _baseService.DeleteEntity(Id);

            if (serviceResult.Code == MISACode.Success)
            {
                return Ok(serviceResult);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Xóa nhiều bản ghi theo Id
        /// </summary>
        /// <param name="listId">Danh sách id bản ghi cần xóa</param>
        /// <returns>Số bản ghi xóa được</returns>
        [EnableCors("AllowCROSPolicy")]
        [HttpDelete]
        public IActionResult MultipleDelete([FromBody]IEnumerable<Guid> listId)
        {
            var serviceResult = _baseService.MultipleDelete(listId);

            if (serviceResult.Code == MISACode.Success)
            {
                return Ok(serviceResult);
            }
            else
            {
                return NoContent();
            }
        }
        #endregion
    }
}
