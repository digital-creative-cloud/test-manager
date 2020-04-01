using System;
using System.Collections.Generic;
using System.Text;

namespace Teste.Manager.Domain.Enums
{
    public enum DeviceInterface
    {
        //web
        chrome = 1,
        firefox,
        ie,
        safari,

        //Mobile
        android,
        ios,

        //DB
        sqlserver,
        mysql,
        postgress,
        oracle
    }
}
