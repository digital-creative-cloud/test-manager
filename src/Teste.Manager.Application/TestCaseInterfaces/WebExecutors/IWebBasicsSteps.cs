using OpenQA.Selenium;

namespace Teste.Manager.Application
{
    public interface IWebBasicsSteps
    {
        void CapturaImagem();
        bool Click(string path);
        string GetText(string path);
        bool SetText(string path, string value);

        void InitClass(IWebDriver driver);
    }
}