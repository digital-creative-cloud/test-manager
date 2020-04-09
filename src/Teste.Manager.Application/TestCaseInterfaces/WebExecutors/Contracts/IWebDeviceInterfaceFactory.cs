using OpenQA.Selenium;
using Teste.Manager.Domain.Enums;

namespace Teste.Manager.Application
{
    public interface IWebDeviceInterfaceFactory
    {
        IWebDriver GetWebDriver(DeviceInterface type);
    }
}