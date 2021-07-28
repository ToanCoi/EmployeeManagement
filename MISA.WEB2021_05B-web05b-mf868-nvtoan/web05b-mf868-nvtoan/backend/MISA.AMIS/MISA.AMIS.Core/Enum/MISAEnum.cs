using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Enum
{
    public enum MISACode
    {
        ValidData = 100,
        InvalidData = 900,
        Exception = 500,
        Success = 200
    }

    public enum EntityState
    {
        Add = 1,
        Update = 2,
        Delete = 3
    }
}
