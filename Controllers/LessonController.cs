using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TSC_CMS.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TSC_CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {

        //c# 宣告方式：變數 型態
        private readonly TSC_SQLContext _tscSql;


        //建構子與類別名稱相同
        //C# 函式宣告方式： [funcName](型態 參數)
        public LessonController(TSC_SQLContext tscSql)
        {
            // 取得資料庫
            _tscSql = tscSql;
        }

        // GET: api/<studentsController1>
        [HttpGet]
        public ActionResult<IEnumerable<Lesson>> Get()
        {
            //TSC_SQLContext 中有 Student and Lesson
            return _tscSql.Lessons.ToList();
        }
        // GET api/<LessonController>/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Lesson>> Get(int StudentId)
        {
            var result = from a in _tscSql.Lessons
                         where a.StudentId == StudentId
                         select a;
            if (result == null)
            {
                return NotFound("找無");
            }

            return result.ToList();
        }

        // POST api/<LessonController>
        [HttpPost]
        public ActionResult<Lesson> Post([FromBody] Lesson value)
        {
            _tscSql.Lessons.Add(value);
            _tscSql.SaveChanges();

            // 新增玩之後用取單筆的方式回傳
            return CreatedAtAction(nameof(Get), new { id = value.Id }, value);

        }

        // PUT api/<LessonController>/5
        [HttpPut("{id}")]
        public ActionResult<Lesson> Put(int id, [FromBody] Lesson value)
        {
            _tscSql.Entry(value).State = EntityState.Modified;

            try
            {
                _tscSql.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "存取發生錯誤");
            }
            return NoContent();
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
