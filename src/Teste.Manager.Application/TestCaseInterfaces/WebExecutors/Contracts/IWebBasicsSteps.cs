using OpenQA.Selenium;

namespace Teste.Manager.Application
{
    public interface IWebBasicsSteps
    {
        void CapturaImagem();
        bool Click(string path);
        string GetText(string path);
        bool SetText(string path, string value);
        bool SetTextMask(string path, string value);
        bool SetSelect(string path, string value);
        bool PressKey(string key);


        void InitClass(IWebDriver driver, string evidenceFolder);
    }
}