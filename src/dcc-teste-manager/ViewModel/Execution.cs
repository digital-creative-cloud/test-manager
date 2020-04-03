using System;
using Newtonsoft;
using Newtonsoft.Json;

namespace dcc_teste_manager
{
    public class Execution
    {
        public string feature { get; set; }
        public string testCase { get; set; }
        public string environment { get; set; }
        public object Data { get; set; }
    }
}
