using Defender.GeneralTestingService.Application.Models;

namespace Defender.GeneralTestingService.Infrastructure.Steps.Interfaces;

public interface IStep
{
    Task<TestInstance> StartAsync(TestInstance instance);
}
