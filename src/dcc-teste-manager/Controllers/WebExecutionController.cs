using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Teste.Manager.Application;
using Teste.Manager.Domain;

namespace dcc_teste_manager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebExecutionController : ControllerBase
    {
        private readonly ILogger<WebExecutionController> _logger;
        private readonly IEngine _engine;
        

        public WebExecutionController(ILogger<WebExecutionController> logger
                                        , IEngine engine)
        {
            _logger = logger;
            _engine = engine;
        }

        [HttpPost]
        public IActionResult Execute([FromBody] Execution execution)
        {
            var json = execution.Data.ToString();

            JObject ret = null;

            if (!String.IsNullOrEmpty(execution.feature))
            {
                ret = _engine.Execute(execution.feature, json, execution.environment);
            }
            else if (!String.IsNullOrEmpty(execution.testCase))
            {
                ret = _engine.Execute(execution.feature, json, execution.environment);
            }

            return Ok(ret.ToString());
        }
    }
}
