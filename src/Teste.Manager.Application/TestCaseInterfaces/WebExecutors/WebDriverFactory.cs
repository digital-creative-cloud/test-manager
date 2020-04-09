using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Text;
using Teste.Manager.Domain.Enums;

namespace Teste.Manager.Application
{
    public class WebDeviceInterfaceFactory : IWebDeviceInterfaceFactory
    {
        public IWebDriver GetWebDriver(DeviceInterface type)
        {
            IWebDriver webDriver;

            switch (type)
            {
                case DeviceInterface.chrome:
                    webDriver = new ChromeDriver();
                    break;
                case DeviceInterface.firefox:
                    webDriver = new FirefoxDriver();
                    break;
                default:
                    throw new NotImplementedException("Device Type not avalible for WebDeviceInterfaceFactory");

            }
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(100);

            return webDriver;
        }
    }
}
