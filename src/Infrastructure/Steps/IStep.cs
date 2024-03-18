using Defender.GeneralTestingService.Application.Models;

namespace Defender.GeneralTestingService.Infrastructure.Steps;

public interface IStep
{
    Task<TestInstance> StartAsync(TestInstance instance);
}
