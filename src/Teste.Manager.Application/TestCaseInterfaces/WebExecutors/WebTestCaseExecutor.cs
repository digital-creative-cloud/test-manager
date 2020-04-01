﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public JObject Execute(TestCase test, string json)
        {
            IWebDriver driver = _webDeviceInterfaceFactory.GetWebDriver(test.DeviceInterface);

            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl(test.ProcessBeginning);

            var collection = ExecuteSteps(driver, test.TestCasesToSteps.OrderBy(x => x.StepOrder).ToList(), json);

            var ret = new JObject();

            foreach (var item in collection)
            {
                ret.Add(item.Key, item.Value);
            }

            driver.Close();

            return ret;
        }

        private List<KeyValuePair<string, string>> ExecuteSteps(IWebDriver driver, List<TestCasesToSteps> collection, string json)
        {
            var data = (JObject)JsonConvert.DeserializeObject(json);

            var ret = new List<KeyValuePair<string, string>>();

            _webBasicsSteps.InitClass(driver);

            foreach (var item in collection)
            {
                string value = data[item.Step.Value].Value<string>();

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
                }
            }

            return ret;
        }
    }
}
