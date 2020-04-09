using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Teste.Manager.Application.TestCaseInterfaces.DBExecutors;

namespace Teste.Manager.Application
{
    public class DbBasicsSteps : IDbBasicsSteps
    {
        private DbCommand _driver;

        public DbBasicsSteps()
        {

        }

        public void InitClass(DbCommand driver)
        {
            _driver = driver;
        }

        public List<JObject> GetText(DbDataReader reader, string[] columns)
        {
            var ret = new List<JObject>();

            while (reader.Read())
            {
                var item = new JObject();

                foreach (var column in columns)
                {
                    var value = reader[column].ToString();

                    item.Add(column, value);
                }

                ret.Add(item);
            }

            return ret;
        }
        public bool SetCommand(string command)
        {
            _driver.CommandText = command;

            return true;
        }

        public DbDataReader Execute()
        {
            try
            {
                _driver.Connection.Open();

                var reader = _driver.ExecuteReader();

                return reader;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool SetParameter(string path, string value)
        {
            DbParameter param = new SqlParameter();
            param.ParameterName = $"@{path}";
            param.Value = value;

            _driver.Parameters.Add(param);

            return true;
        }
    }
}
