using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Teste.Manager.Application.TestCaseInterfaces.Factory;
using Teste.Manager.DataAccess;
using Teste.Manager.Domain;

namespace Teste.Manager.Application
{
    public class Engine : IEngine
    {
        private readonly IExecutionSteps _executionSteps;
        private readonly IDeviceTypeExecutorFactory _deviceTypeExecutorFactory;

        public Engine(IExecutionSteps executionSteps,
                      IDeviceTypeExecutorFactory deviceTypeExecutorFactory)
        {
            _executionSteps = executionSteps;
            _deviceTypeExecutorFactory = deviceTypeExecutorFactory;
        }

        public JObject Execute(string executionName, string json, string env)
        {
            var feature = _executionSteps.GetFeatureByName(executionName);

            var ret = new JObject();

            foreach (var item in feature.FeaturesToTestCases)
            {
                saveReturn(json, item.TestCase, ret, env);
            }

            return ret;
        }

        public JObject ExecuteTestCase(string TestCaseName, string json, string env)
        {
            var item = _executionSteps.GetTestCaseByName(TestCaseName);

            var ret = new JObject();

            saveReturn(json, item, ret, env);

            return ret;
        }

        private void saveReturn(string json, TestCase item, JObject ret, string env)
        {
            var testResult = proceedWithTestCase(item, json, env);

            ret.Add(item.Name, testResult);
        }

        private JObject proceedWithTestCase(TestCase test, string json, string env)
        {
            var executor = _deviceTypeExecutorFactory.GetExecutor(test.DeviceType);

            return executor.Execute(test, json, env);
        }
    }
}
