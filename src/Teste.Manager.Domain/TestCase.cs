using System;
using System.Collections.Generic;
using System.Text;
using Teste.Manager.Domain.Enums;

namespace Teste.Manager.Domain
{
    public class TestCase
    {
        public int Id { get; set; }
        public string ProcessBeginning { get; set; }
        public string Name { get; set; }
        public DeviceType DeviceType { get; set; }
        public DeviceInterface DeviceInterface { get; set; }
        public virtual List<FeaturesToTestCases> FeaturesToTestCases { get; set; }
        public virtual List<TestCasesToSteps> TestCasesToSteps { get; set; }
    }
}
