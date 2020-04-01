using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Teste.Manager.Domain;

namespace Teste.Manager.DataAccess
{
    public class ExecutionSteps : IExecutionSteps
    {
        private readonly IServiceProvider _serviceProvider;
        public ExecutionSteps(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public Feature GetFeatureByName(string featureName)
        {
            using (var context = new TestManagerContext(
                _serviceProvider.GetRequiredService<
                    DbContextOptions<TestManagerContext>>()))
            {

                return context.Feature.Where(x => x.Name.Equals(featureName))
                    .Include(x => x.FeaturesToTestCases)
                    .ThenInclude(x => x.TestCase)
                    .ThenInclude(x => x.TestCasesToSteps)
                    .ThenInclude(x => x.Step)
                    .First();
            }
        }

        public TestCase GetTestCaseByName(string testCaseName)
        {
            using (var context = new TestManagerContext(
                _serviceProvider.GetRequiredService<
                    DbContextOptions<TestManagerContext>>()))
            {

                return context.TestCase.Where(x => x.Name.Equals(testCaseName))
                    .Include(x => x.TestCasesToSteps)
                    .ThenInclude(x => x.Step).First();
            }
        }
    }
}
