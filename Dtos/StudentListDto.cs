using System.ComponentModel.DataAnnotations;

namespace TSC_CMS.Dtos
{
    public partial class StudentListDto
    {
        //[Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
