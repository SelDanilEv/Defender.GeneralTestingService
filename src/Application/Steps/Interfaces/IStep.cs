using Defender.GeneralTestingService.Application.Models;

namespace Defender.GeneralTestingService.Application.Steps.Interfaces;

public interface IStep
{
    Task<TestInstance> StartAsync(TestInstance instance);
}
