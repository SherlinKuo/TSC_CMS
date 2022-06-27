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
        public ActionResult<LessonDetailDto> Get()
        {
            //TSC_SQLContext 中有 Student and Lesson
            List<Lesson> orgData = Enumerable.Reverse(_tscSql.Lessons).Take(10).ToList();
            List<Action> actionList = _tscSql.Actions.ToList();
            IEnumerable<StudentListDto> student = _tscSql.Students.ToList().Select(a => new StudentListDto
               {
                   Id = a.Id,
                   Name = a.Name,
               });
            //Console.Write(studentListDtos);
            LessonDetailDto lessonDetailDto = new LessonDetailDto();
            List<LessonDetail> lessonDetailList = new List<LessonDetail>();
            foreach (var item in orgData)
            {
                LessonDetail temp = new LessonDetail
                {
                    Id = item.Id,
                    Date = item.Date,
                    Lesson1 = item.Lesson1 % 8 == 0 ? 8 : item.Lesson1 % 8,
                    Action = actionList.SingleOrDefault(ele => ele.Id == item.Action).Action1,
                    Student = student.ToList().SingleOrDefault(ele => ele.Id == item.StudentId).Name
                    
                };

                lessonDetailList.Add(temp);
            }
            lessonDetailDto.Lessons = lessonDetailList;

            return lessonDetailDto;
        }
        // GET api/<LessonController>/5
        [HttpGet("{id}")]
        public ActionResult<LessonDetailDto> Get(int id)
        {
            var result = from a in _tscSql.Lessons
                         where a.StudentId == id
                         select a;
            List<Lesson> orgData = Enumerable.Reverse(result).Take(10).ToList();
            List<Action> actionList = _tscSql.Actions.ToList();
            IEnumerable<StudentListDto> student = _tscSql.Students.ToList().Select(a => new StudentListDto
            {
                Id = a.Id,
                Name = a.Name,
            });
            LessonDetailDto lessonDetailDto = new LessonDetailDto();
            List<LessonDetail> lessonDetailList = new List<LessonDetail>();
            foreach (var item in orgData)
            {
                LessonDetail temp = new LessonDetail
                {
                    Id = item.Id,
                    Date = item.Date,
                    Lesson1 = item.Lesson1 % 8 == 0 ? 8 : item.Lesson1 % 8,
                    Action = actionList.SingleOrDefault(ele => ele.Id == item.Action).Action1,
                    Student = student.ToList().SingleOrDefault(ele => ele.Id == item.StudentId).Name

                };

                lessonDetailList.Add(temp);
            }
            lessonDetailDto.Lessons = lessonDetailList;
            //if (result == null)
            //{
            //    return NotFound("找無");
            //}

            int lessonCounter = result.Where(ele => ele.Action == 1).ToList().Count;
            
            int payCounter = result.Where(ele => ele.Action == 4).ToList().Count;
            if (lessonCounter / 8 + 1 > payCounter)
            {
                lessonDetailDto.PayStatus = 8;
            }else if((lessonCounter + 1) / 8 + 1 > payCounter)
            {
                lessonDetailDto.PayStatus = 7;
            }

            return lessonDetailDto;
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
                Lesson1 = value.Action == 1 ? result.Count() + 1 : result.Count()
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
