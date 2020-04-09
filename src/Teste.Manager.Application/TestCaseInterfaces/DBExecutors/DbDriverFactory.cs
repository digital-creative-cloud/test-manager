using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using Teste.Manager.Application.TestCaseInterfaces.DBExecutors;
using Teste.Manager.Domain.Enums;

namespace Teste.Manager.Application
{
    public class DbDeviceInterfaceFactory : IDbDeviceInterfaceFactory
    {
        public DbCommand GetDbDriver(DeviceInterface type, string connection)
        {
            DbCommand dbDriver;

            switch (type)
            {
                case DeviceInterface.sqlserver:
                    dbDriver = new SqlCommand();
                    dbDriver.Connection = new SqlConnection(connection);

                    break;
                default:
                    throw new NotImplementedException("Device Type not avalible for WebDeviceInterfaceFactory");

            }
            return dbDriver;
        }
    }
}
