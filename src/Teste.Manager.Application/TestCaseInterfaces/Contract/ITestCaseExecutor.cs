using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Teste.Manager.Domain;

namespace Teste.Manager.Application.TestCaseInterfaces.Contract
{
    public interface ITestCaseExecutor
    {
        JObject Execute(TestCase test, string json);
    }
}
