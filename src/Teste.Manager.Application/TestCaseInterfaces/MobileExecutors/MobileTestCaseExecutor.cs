using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Teste.Manager.Application.TestCaseInterfaces.Contract;
using Teste.Manager.Domain;

namespace Teste.Manager.Application.TestCaseInterfaces.MobileExecutors
{
    public class MobileTestCaseExecutor : ITestCaseExecutor
    {
        public JObject Execute(TestCase test, string json)
        {
            throw new NotImplementedException();
        }
    }
}
