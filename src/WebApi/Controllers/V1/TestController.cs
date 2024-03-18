using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Defender.GeneralTestingService.Application.Models;
using Defender.GeneralTestingService.Application.Common.Interfaces;
using System.Collections.Generic;

namespace Defender.GeneralTestingService.WebUI.Controllers.V1;

public class TestController : BaseApiController
{
    private readonly ITestStartingService _testStartingService;

    public TestController(
        IMediator mediator, 
        IMapper mapper,
        ITestStartingService testStartingService) 
        : base(mediator, mapper)
    {
        _testStartingService = testStartingService;
    }

    [HttpPost("start")]
    //[Auth(Roles.Admin)]
    [ProducesResponseType(typeof(List<TestInstance.StepLog>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<List<TestInstance.StepLog>> StartTest()
    {
        var instance = TestInstance.Init(HttpContext);

        await _testStartingService.StartFullTestAsync(instance);

        return instance.GetLog;
    }

}
