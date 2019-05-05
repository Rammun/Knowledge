using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class AccountController : ApiBaseController
    {
        [HttpGet]
        public ActionResult<string> Login(int id)
        {
            return "value";
        }
    }
}