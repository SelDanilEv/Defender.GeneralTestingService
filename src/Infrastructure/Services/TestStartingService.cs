using Defender.GeneralTestingService.Application.Common.Interfaces;
using Defender.GeneralTestingService.Application.Models;
using Defender.GeneralTestingService.Infrastructure.Steps;
using Defender.GeneralTestingService.Infrastructure.Steps.Interfaces;
using Defender.GeneralTestingService.Infrastructure.Steps.Sets;

namespace Defender.GeneralTestingService.Infrastructure.Services;

public class TestStartingService : ITestStartingService
{
    private readonly List<IStep> _steps;

    public TestStartingService(RegressionSet allStepsSet)
    {
        _steps = allStepsSet;
    }

    public async Task StartFullTestAsync(TestInstance instance)
    {
        var tasks = new List<Task>();

        foreach(var step in _steps)
        {
            if (step is IParallelStep)
            {
                tasks.Add(step.StartAsync(instance));
            }
            else
            {
                await step.StartAsync(instance);
            }
        }

        Task.WaitAll(tasks.ToArray());
    }
}
