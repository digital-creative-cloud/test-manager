using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Teste.Manager.Application
{
    public class WebBasicsSteps : IWebBasicsSteps
    {
        private IWebDriver _driver;

        private string screenshotsPasta = "";

        private int contador = 1;

        public WebBasicsSteps()
        {

        }

        public void InitClass(IWebDriver driver, string evidenceFolder)
        {
            _driver = driver;

            screenshotsPasta = evidenceFolder;

            if (!Directory.Exists(screenshotsPasta))
            {
                Directory.CreateDirectory(screenshotsPasta);
            }
        }

        public bool Click(string path)
        {
            _driver.FindElement(By.XPath(path)).Click();
            CapturaImagem();
            return true;
        }

        private void Screenshot(string screenshotsPasta)
        {
            ITakesScreenshot camera = _driver as ITakesScreenshot;
            Screenshot foto = camera.GetScreenshot();
            foto.SaveAsFile(screenshotsPasta, ScreenshotImageFormat.Png);
        }

        public void CapturaImagem()
        {
            Screenshot(screenshotsPasta + "Imagem_" + contador++ + ".png");
        }

        public bool SetText(string path, string value)
        {
            foreach (var item in value)
            {
                _driver.FindElement(By.XPath(path)).SendKeys(item.ToString());
                Thread.Sleep(300);
            }

            CapturaImagem();

            return true;
        }

        public string GetText(string path)
        {
            var element = _driver.FindElement(By.XPath(path));

            var value = element.GetAttribute("value");

            CapturaImagem();

            return value;
        }
    }
}
