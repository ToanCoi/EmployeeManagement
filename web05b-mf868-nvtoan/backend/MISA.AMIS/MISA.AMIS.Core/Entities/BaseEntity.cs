﻿using MISA.AMIS.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    [AttributeUsage(AttributeTargets.Property)]
    public class Required : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class Unique : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKey : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class ForeignKey : Attribute { }

    public class BaseEntity
    {
        /// <summary>
        /// Trạng thái của Entity
        /// </summary>
        public EntityState EntityState { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Trạng thái insert của entity
        /// </summary>
        public List<string> Status { get; set; }
    }
}
