using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.API.Controllers
{
    public class DepartmentsController : BaseController<Department>
    {
        #region Constructor
        public DepartmentsController(IDepartmentService departmentService) : base(departmentService)
        {

        }
        #endregion
    }
}
