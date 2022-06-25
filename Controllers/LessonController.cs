using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TSC_CMS.Dtos;
using TSC_CMS.Models;
using Action = TSC_CMS.Models.Action;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TSC_CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {

        //c# 宣告方式：變數 型態
        private readonly TSC_SQLContext _tscSql;
        private readonly IHttpContextAccessor _httpContextAccessor;

        //建構子與類別名稱相同
        //C# 函式宣告方式： [funcName](型態 參數)
        public LessonController(TSC_SQLContext tscSql)
        {
            // 取得資料庫
            _tscSql = tscSql;
        }

        // GET: api/<studentsController1>
        [HttpGet]
        public ActionResult<IEnumerable<LessonDetailDto>> Get()
        {
            //TSC_SQLContext 中有 Student and Lesson
            List<Lesson> orgData = _tscSql.Lessons.ToList();
            IEnumerable<StudentListDto> student = _tscSql.Students.ToList().Select(a => new StudentListDto
               {
                   Id = a.Id,
                   Name = a.Name,
               });
            //Console.Write(studentListDtos);
            List<LessonDetailDto> lessonDetailDto = new List<LessonDetailDto>();
            foreach (var item in orgData)
            {
                 List<Action> actionList = _tscSql.Actions.ToList();



                LessonDetailDto temp = new LessonDetailDto
                {
                    Id = item.Id,
                    Date = item.Date,
                    Lesson1 = item.Lesson1 % 8 == 0 ? 1 : item.Lesson1 % 8 + 1,
                    Action = actionList[item.Action].Action1,
                    Student = student.ToList().SingleOrDefault(ele => ele.Id == item.StudentId).Name
                    //[item.StudentId].Name
                };

                lessonDetailDto.Add(temp);
            }

            return lessonDetailDto;
        }
        // GET api/<LessonController>/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Lesson>> Get(int id)
        {
            var result = from a in _tscSql.Lessons
                         where a.StudentId == id
                         select a;
            if (result == null)
            {
                return NotFound("找無");
            }

            return result.ToList();
        }

        // POST api/<LessonController>
        [HttpPost]
        public ActionResult<Lesson> Post([FromBody] LessonAddDto value)
        {
            var result = from a in _tscSql.Lessons
                         where a.StudentId == value.StudentId && a.Action == 1 
                         select a;
            Lesson LessonData = new Lesson
            {
                StudentId = value.StudentId,
                Date = value.Date,
                Action = value.Action,
                Lesson1 = value.Action == 1 ? result.Count() : result.Count() + 1
            };
            Console.Write(LessonData);
            _tscSql.Lessons.Add(LessonData);
            _tscSql.SaveChanges();

            // 新增玩之後用取單筆的方式回傳
            return CreatedAtAction(nameof(Get), new { id = value.Id }, value);

        }


        // DELETE api/<LessonController>/5
        [HttpDelete("{id}")]
        public ActionResult<Lesson> Delete(int id)
        {

            var delete = _tscSql.Lessons.Find(id);

            if (delete == null)
            {
                return NotFound();
            }

            _tscSql.Lessons.Remove(delete);
            _tscSql.SaveChanges();
            return NoContent();
        }
    }
}
