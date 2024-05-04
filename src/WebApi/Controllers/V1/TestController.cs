using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Defender.GeneralTestingService.Application.Models;
using Defender.GeneralTestingService.Application.Common.Interfaces;
using System.Collections.Generic;
using Defender.Common.Helpers;
using Microsoft.Extensions.Configuration;

namespace Defender.GeneralTestingService.WebUI.Controllers.V1;

public class TestController(
    IConfiguration configuration,
    IMediator mediator,
    IMapper mapper,
    ITestStartingService testStartingService)
        : BaseApiController(mediator, mapper)
{

    [HttpPost("start")]
    //[Auth(Roles.Admin)]
    [ProducesResponseType(typeof(List<TestInstance.StepLog>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<List<TestInstance.StepLog>> StartTest()
    {
        var instance = TestInstance.Init(HttpContext);

        await testStartingService.StartFullTestAsync(instance);

        return instance.GetLog;
    }

    [HttpGet("get/superadmin-jwt")]
    //[Auth(Roles.Admin)]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<string> GetSuperAdminJwtAsync([FromQuery] int minutes = 1)
    {
        return await InternalJwtHelper.GenerateInternalJWTAsync(
            configuration["JwtTokenIssuer"] ?? string.Empty, minutes);
    }
}
