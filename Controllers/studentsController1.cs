using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TSC_CMS.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TSC_CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class studentsController1 : ControllerBase
    {
        //c# 宣告方式：變數 型態
        private readonly TSC_SQLContext _tscSql;


        //建構子與類別名稱相同
        //C# 函式宣告方式： [funcName](型態 參數)
        public studentsController1(TSC_SQLContext tscSql)
        {
            // 取得資料庫
            _tscSql = tscSql;
        }


        // GET: api/<studentsController1>
        [HttpGet]
        public ActionResult<IEnumerable<Student>> Get()
        {
            //TSC_SQLContext 中有 Student and Lesson
            return _tscSql.Students.ToList();
        }

        //取單筆資料
        // GET api/<studentsController1>/5
        [HttpGet("{id}")]
        // ActionResult：會回傳狀態
        public   ActionResult<Student> Get(int id)
        {
            var result = _tscSql.Students.Find(id);

            return result;
        }

        // POST api/<studentsController1>
        [HttpPost]
        public ActionResult<Student> Post([FromBody] Student value)
        {
            _tscSql.Students.Add(value);
            _tscSql.SaveChanges();

            // 新增玩之後用取單筆的方式回傳
            return CreatedAtAction(nameof(Get), new {id = value.Id}, value);
        }

        // PUT api/<studentsController1>/5
        [HttpPut]
        public ActionResult<Student> Put([FromBody] Student value)
        {
            //if(id != value.Id)
            //{
            //    return BadRequest();
            //}

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

        // DELETE api/<studentsController1>/5
        [HttpDelete("{id}")]
        public ActionResult<Student> Delete(int id)
        {
            var delete = _tscSql.Students.Find(id);

            if (delete == null)
            {
                return NotFound();
            }

            _tscSql.Students.Remove(delete);
            _tscSql.SaveChanges();
            return NoContent();
        }
    }
}
