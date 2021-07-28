using MISA.AMIS.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    public class ServiceResult
    {
        public Object Data { get; set; }
        public string Message { get; set; }
        public MISACode Code { get; set; }
    }
}
