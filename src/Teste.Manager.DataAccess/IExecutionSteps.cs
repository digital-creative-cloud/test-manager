using System.Collections.Generic;
using Teste.Manager.Domain;

namespace Teste.Manager.DataAccess
{
    public interface IExecutionSteps
    {
        Feature GetFeatureByName(string featureName);
        TestCase GetTestCaseByName(string testCaseName);
    }
}