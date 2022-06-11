using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TSC_CMS.Models
{
    public partial class Action
    {
        /// <summary>
        /// 紀錄 Id
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 紀錄
        /// </summary>
        public string Action1 { get; set; } = null!;
    }
}
