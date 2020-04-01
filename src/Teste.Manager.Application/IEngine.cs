using Newtonsoft.Json.Linq;

namespace Teste.Manager.Application
{
    public interface IEngine
    {
        JObject Execute(string executionName, string json);
        JObject ExecuteTestCase(string TestCaseName, string json);
    }
}