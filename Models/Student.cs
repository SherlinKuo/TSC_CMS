using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TSC_CMS.Models
{
    public partial class Student
    {
        /// <summary>
        /// 學生 Id
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 學生名
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 學生中文
        /// </summary>
        public string? NameEn { get; set; }
        /// <summary>
        /// 聯絡電話
        /// </summary>
        public string? Phone { get; set; }
        /// <summary>
        /// 上課時段
        /// </summary>
        public string TimePeriod { get; set; } = null!;
        /// <summary>
        /// 備註
        /// </summary>
        public string? Note { get; set; }
    }
}
