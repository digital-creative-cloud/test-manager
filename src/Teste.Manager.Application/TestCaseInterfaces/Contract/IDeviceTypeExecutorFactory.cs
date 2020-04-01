using Teste.Manager.Application.TestCaseInterfaces.Contract;
using Teste.Manager.Domain.Enums;

namespace Teste.Manager.Application.TestCaseInterfaces.Factory
{
    public interface IDeviceTypeExecutorFactory
    {
        ITestCaseExecutor GetExecutor(DeviceType type);
    }
}