using Microsoft.AspNetCore.Mvc;
using TSC_CMS.Models;
using Action = TSC_CMS.Models.Action;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TSC_CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionList : ControllerBase
    {


        //c# 宣告方式：變數 型態
        private readonly TSC_SQLContext _tscSql;


        //建構子與類別名稱相同
        //C# 函式宣告方式： [funcName](型態 參數)
        public ActionList(TSC_SQLContext tscSql)
        {
            // 取得資料庫
            _tscSql = tscSql;
        }

        // GET: api/<ActionList>
        [HttpGet]
        public ActionResult<IEnumerable<Action>> Get()
        {
            return _tscSql.Actions.ToList();
        }


    }
}
