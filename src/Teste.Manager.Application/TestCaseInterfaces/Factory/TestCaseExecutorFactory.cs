using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teste.Manager.Application.TestCaseInterfaces.Contract;
using Teste.Manager.Application.TestCaseInterfaces.WebExecutors;
using Teste.Manager.Domain.Enums;

namespace Teste.Manager.Application.TestCaseInterfaces.Factory
{
    public class DeviceTypeExecutorFactory : IDeviceTypeExecutorFactory
    {
        private readonly IEnumerable<ITestCaseExecutor> _itestCaseExecutor;
        public DeviceTypeExecutorFactory(IEnumerable<ITestCaseExecutor> itestCaseExecutor)
        {
            _itestCaseExecutor = itestCaseExecutor;
        }

        public ITestCaseExecutor GetExecutor(DeviceType type)
        {
            switch (type)
            {
                case DeviceType.web:
                    return _itestCaseExecutor.FirstOrDefault(x => x.GetType().Name.Equals(nameof(WebTestCaseExecutor)));
                case DeviceType.database:
                    return _itestCaseExecutor.FirstOrDefault(x => x.GetType().Name.Equals(nameof(DbTestCaseExecutor)));

                default:
                    return null;
            }
        }
    }
}
