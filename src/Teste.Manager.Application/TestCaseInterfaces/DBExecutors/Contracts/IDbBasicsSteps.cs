using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Teste.Manager.Application.TestCaseInterfaces.DBExecutors
{
    public interface IDbBasicsSteps
    {
        DbDataReader Execute();
        bool SetParameter(string path, string value);
        bool SetCommand(string command);
        List<JObject> GetText(DbDataReader reader, string[] columns);
        void InitClass(DbCommand Command);
    }
}
