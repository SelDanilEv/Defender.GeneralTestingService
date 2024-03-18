using Defender.GeneralTestingService.Application.Common.Interfaces;
using Defender.GeneralTestingService.Application.Models;
using Defender.GeneralTestingService.Infrastructure.Steps;
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
        foreach(var step in _steps)
        {
            await step.StartAsync(instance);
        }
    }
}
