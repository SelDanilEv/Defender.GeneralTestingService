using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Defender.Common.Attributes;
using Defender.Common.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Defender.Common.Interfaces;
using Defender.Common.Helpers;

namespace Defender.GeneralTestingService.WebUI.Controllers.V1;

public class HomeController : BaseApiController
{
    private readonly ICurrentAccountAccessor _accountAccessor;

    public HomeController(
        ICurrentAccountAccessor accountAccessor,
        IMediator mediator,
        IMapper mapper)
        : base(mediator, mapper)
    {
        _accountAccessor = accountAccessor;
    }

    [HttpGet("health")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<object> HealthCheckAsync()
    {
        return new { Status = "Healthy" };
    }

    public record AuthCheckResponse(System.Guid UserId, string HighestRole);

    [HttpGet("authorization/check")]
    [Auth(Roles.User)]
    [ProducesResponseType(typeof(AuthCheckResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<AuthCheckResponse> AuthorizationCheckAsync()
    {
        var userId = _accountAccessor.GetAccountId();
        var userRoles = _accountAccessor.GetRoles();

        return new AuthCheckResponse(userId, RolesHelper.GetHighestRole(userRoles));
    }
}
