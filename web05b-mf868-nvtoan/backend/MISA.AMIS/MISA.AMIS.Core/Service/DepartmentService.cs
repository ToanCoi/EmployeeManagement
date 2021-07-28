using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interface.Repository;
using MISA.AMIS.Core.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Service
{
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {
        #region Constructor
        public DepartmentService(IDepartmentRepository departmentRepository) : base(departmentRepository)
        {

        }
        #endregion
    }
}
