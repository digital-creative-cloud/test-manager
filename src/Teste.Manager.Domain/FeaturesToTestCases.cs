using System;
using System.Collections.Generic;
using System.Text;

namespace Teste.Manager.Domain
{
    public class FeaturesToTestCases
    {
        public int FeatureId { get; set; }
        public int TestCaseId { get; set; }

        public virtual Feature Feature { get; set; }
        public virtual TestCase TestCase { get; set; }
    }
}
