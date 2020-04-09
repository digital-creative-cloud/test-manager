using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Teste.Manager.Domain.Enums;

namespace Teste.Manager.Application.TestCaseInterfaces.DBExecutors
{
    public interface IDbDeviceInterfaceFactory
    {
        DbCommand GetDbDriver(DeviceInterface type, string connection);
    }
}
