﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CallAsyncMethodsParallel.Api.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("Home")]
        public IActionResult Home()
        {
            return Ok(new
            {
                Application = "CallAsyncMethodsParaller"
            }
            );
        }
    }
}
