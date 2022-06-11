using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TSC_CMS.Models
{
    public partial class TimePeriod
    {
        [Key]
        public int Id { get; set; }
        public string TimePeriod1 { get; set; } = null!;
    }
}
