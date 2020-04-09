using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Threading;
using Teste.Manager.Application.TestCaseInterfaces.Contract;
using Teste.Manager.Application.TestCaseInterfaces.DBExecutors;
using Teste.Manager.Domain;
using Teste.Manager.Domain.Enums;

namespace Teste.Manager.Application.TestCaseInterfaces.WebExecutors
{
    public class DbTestCaseExecutor : ITestCaseExecutor
    {
        private readonly IDbDeviceInterfaceFactory _dbDeviceInterfaceFactory;
        private readonly IDbBasicsSteps _dbBasicsSteps;
        public DbTestCaseExecutor(IDbDeviceInterfaceFactory webDeviceInterfaceFactory,
                                  IDbBasicsSteps webBasicsSteps)
        {
            _dbDeviceInterfaceFactory = webDeviceInterfaceFactory;
            _dbBasicsSteps = webBasicsSteps;
        }
        public JObject Execute(TestCase test, string json, string env)
        {
            var ret = new JObject();
            DbCommand driver = _dbDeviceInterfaceFactory.GetDbDriver(test.DeviceInterface, getConfigurationValue(env, "processBeginning", test.ProcessBeginning));

            try
            {
                var collection = ExecuteSteps(driver, test.TestCasesToSteps.OrderBy(x => x.StepOrder).ToList(), json);

                var count = 0;
                foreach (var item in collection)
                {
                    count++;

                    ret.Add($"returnObject{count}", item);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

                driver.Dispose();
            }

            return ret;
        }

        private List<JObject> ExecuteSteps(DbCommand driver, List<TestCasesToSteps> collection, string json)
        {
            var data = (JObject)JsonConvert.DeserializeObject(json);

            var ret = new List<JObject>();
            DbDataReader reader = null;

            _dbBasicsSteps.InitClass(driver);

            foreach (var item in collection)
            {
                string value = "";

                if (item.Step.TypeId.Equals(StepType.setparameter))
                {
                    if (!String.IsNullOrEmpty(item.Step.Value))
                    {
                        value = data[item.Step.Value].Value<string>();
                    }
                }

                switch (item.Step.TypeId)
                {
                    case StepType.setparameter:
                        _dbBasicsSteps.SetParameter(item.Step.Path, value);
                        break;
                    case StepType.execute:
                        reader = _dbBasicsSteps.Execute();
                        break;
                    case StepType.getText:
                        ret = _dbBasicsSteps.GetText(reader, item.Step.Path.Split(';'));
                        break;
                    case StepType.setCommand:
                        _dbBasicsSteps.SetCommand(item.Step.Value);
                        break;
                }

                Thread.Sleep(400);
            }

            return ret;
        }


        private string getConfigurationValue(string env, params string[] path)
        {
            var config = "";
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "Configuracao.json");

            if (File.Exists(FilePath))
            {
                var configFile = (JObject)JsonConvert.DeserializeObject(File.ReadAllText(FilePath));

                var a = configFile["environment"].Select(m => (JObject)m.SelectToken(env)).First();

                JToken b = a;

                foreach (var item in path)
                {
                    b = b.SelectToken(item);
                }

                config = b.Value<string>();
            }

            return config;
        }
    }
}
