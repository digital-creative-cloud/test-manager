using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Teste.Manager.Application.TestCaseInterfaces.Contract;
using Teste.Manager.Domain;
using Teste.Manager.Domain.Enums;

namespace Teste.Manager.Application.TestCaseInterfaces.WebExecutors
{


    public class WebTestCaseExecutor : ITestCaseExecutor
    {
        private readonly IWebDeviceInterfaceFactory _webDeviceInterfaceFactory;
        private readonly IWebBasicsSteps _webBasicsSteps;
        public WebTestCaseExecutor(IWebDeviceInterfaceFactory webDeviceInterfaceFactory,
                                    IWebBasicsSteps webBasicsSteps)
        {
            _webDeviceInterfaceFactory = webDeviceInterfaceFactory;
            _webBasicsSteps = webBasicsSteps;
        }
        public JObject Execute(TestCase test, string json, string env)
        {
            IWebDriver driver = _webDeviceInterfaceFactory.GetWebDriver(test.DeviceInterface);

            driver.Navigate().GoToUrl(getConfigurationValue(env, "processBeginning",test.ProcessBeginning));

            driver.Manage().Window.Maximize();

            var collection = ExecuteSteps(driver, test.TestCasesToSteps.OrderBy(x => x.StepOrder).ToList(), json, env);

            var ret = new JObject();

            foreach (var item in collection)
            {
                ret.Add(item.Key, item.Value);
            }

            driver.Close();

            return ret;
        }

        private List<KeyValuePair<string, string>> ExecuteSteps(IWebDriver driver, List<TestCasesToSteps> collection, string json, string env)
        {
            var data = (JObject)JsonConvert.DeserializeObject(json);

            var ret = new List<KeyValuePair<string, string>>();

            var evidenceFolder = getConfigurationValue(env, "evidenceFolder");

            _webBasicsSteps.InitClass(driver, evidenceFolder);

            foreach (var item in collection)
            {
                string value = "";

                if (!String.IsNullOrEmpty(item.Step.Value))
                {
                    value = data[item.Step.Value].Value<string>();
                }

                switch (item.Step.TypeId)
                {
                    case StepType.setText:
                        _webBasicsSteps.SetText(item.Step.Path, value);
                        break;                   
                    case StepType.getText:
                        value = _webBasicsSteps.GetText(item.Step.Path);
                        ret.Add(new KeyValuePair<string, string>(item.Step.Value, value));
                        break;
                    case StepType.click:
                        _webBasicsSteps.Click(item.Step.Path);
                        break;
                    case StepType.select:
                        _webBasicsSteps.SetSelect(item.Step.Path, value);
                        break;
                    case StepType.setTextMask:
                        _webBasicsSteps.SetTextMask(item.Step.Path, value);
                        break;
                    case StepType.PressKey:
                        _webBasicsSteps.PressKey(value);
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
