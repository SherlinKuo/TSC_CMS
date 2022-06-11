using System.ComponentModel.DataAnnotations;

namespace TSC_CMS.Dtos
{
    public class LessonAddDto
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// 行為(上課、繳費、請假)
        /// </summary>
        public int Action { get; set; }
        public int StudentId { get; set; }
    }
}
