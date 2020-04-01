using System;
using System.Collections.Generic;
using System.Text;

namespace Teste.Manager.Domain
{
    public class TestCasesToSteps
    {
        public int TestCaseId { get; set; }
        public int StepId { get; set; }
        public int StepOrder { get; set; }

        public virtual TestCase TestCase { get; set; }
        public virtual Step Step { get; set; }
    }
}
