using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETChallenge.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETChallenge.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public IServiceUrl Service;
        public ValuesController(IServiceUrl service)
        {
            Service = service;
        }

        public async Task<JsonResult> Get()
        {
            return new JsonResult(new { data = await Service.GetAllAPI() });

        }
    }
}
