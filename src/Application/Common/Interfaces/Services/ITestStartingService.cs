using Defender.GeneralTestingService.Application.Models;

namespace Defender.GeneralTestingService.Application.Common.Interfaces;

public interface ITestStartingService
{
    Task StartFullTestAsync(TestInstance instance);
}
