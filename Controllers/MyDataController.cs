
using Microsoft.AspNetCore.Mvc;
using ReserveItTest.Repository;
using System.Collections.Generic;

namespace ReserveItTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MyDataController : ControllerBase
    {
        private readonly IStringStore _store;
        public MyDataController(IStringStore store)
        {
            _store = store;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var data = _store.GetList();
            return new OkObjectResult(data);
        }
        [HttpPost]
        public ActionResult Post([FromBody] List<string> content)
        {
            
            var header = Request.Headers;
            if (!header.ContainsKey("page-size"))
            {
                return StatusCode(400);
            }
            var pa = header.TryGetValue("page-size", out var  PageSize);
            var ps = int.Parse(PageSize);
            if (ps<=0)
            {
                return StatusCode(400);
            }
            if (content.Count <= 0 || content == null)
            {
                return StatusCode(400);
            }
            var filtereddata = new List<string>();
            foreach (var item in content)
            {
                if (item.Length <= ps)
                {
                    filtereddata.Add(item);
                }
            }
           _store.StoreData(filtereddata);
            return StatusCode(201);

        }
    }
}
