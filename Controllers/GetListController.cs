using Microsoft.AspNetCore.Mvc;
using TSC_CMS.Dtos;
using TSC_CMS.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TSC_CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetListController : ControllerBase
    {
        //c# 宣告方式：變數 型態
        private readonly TSC_SQLContext _tscSql;


        //建構子與類別名稱相同
        //C# 函式宣告方式： [funcName](型態 參數)
        public GetListController(TSC_SQLContext tscSql)
        {
            // 取得資料庫
            _tscSql = tscSql;
        }


        // GET: api/<GetListController>

        [HttpGet]
        public IEnumerable<StudentListDto> Get()
        {
            //TSC_SQLContext 中有 Student and Lesson
            return _tscSql.Students.ToList()
                .Select(a => new StudentListDto
            {
                Id=a.Id,
                Name=a.Name,   
            });
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<StudentListDto>> Get(int id)
        {
            var result = from a in _tscSql.Students
                         where a.TimePeriod == id
                         select a;
            return result.ToList()
                .Select(a => new StudentListDto
                {
                    Id = a.Id,
                    Name = a.Name,
                }).ToList();
        }

    }
}
