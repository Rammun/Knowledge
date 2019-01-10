using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
	//[Route("api/[controller]")]
	//[ApiController]
	public class ValuesController : ApiBaseController
    {
		// GET api/values
		//[HttpGet]
		//public ActionResult<IEnumerable<string>> Get()
		//{
		//	return new string[] { "value1", "value2" };
		//}

		// GET api/values/5
		[HttpGet]
		public ActionResult<string> GetById(int id)
		{
			return "value";
		}

		// POST api/values
		[HttpPost]
		public void Create([FromBody] string value)
		{
		}

		// PUT api/values/5
		[HttpPut]
		public void Update(int id, [FromBody] string value)
		{
		}

		// DELETE api/values/5
		[HttpDelete]
		public void Remove(int id)
		{
		}

        //[Route("/identity")]
        [Authorize]
		[HttpGet]
        public IActionResult Identity()
		{
			return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
		}
    }
}
