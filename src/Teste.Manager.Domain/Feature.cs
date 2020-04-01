using System;
using System.Collections.Generic;
using System.Text;

namespace Teste.Manager.Domain
{
    public class Feature
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Describe { get; set; }
        public virtual List<FeaturesToTestCases> FeaturesToTestCases { get; set; }
    }
}
