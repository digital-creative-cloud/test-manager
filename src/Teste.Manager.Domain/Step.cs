using System;
using System.Collections.Generic;
using System.Text;
using Teste.Manager.Domain.Enums;

namespace Teste.Manager.Domain
{
    public class Step
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public StepType TypeId { get; set; }
        public string Value { get; set; }
        public string Path { get; set; }
        public virtual List<TestCasesToSteps> TestCasesToSteps { get; set; }
    }
}
