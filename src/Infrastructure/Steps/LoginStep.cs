using Defender.GeneralTestingService.Application.Models;
using Defender.GeneralTestingService.Infrastructure.Clients.Portal;
using Defender.GeneralTestingService.Infrastructure.Helpers.LocalSecretHelper;
using Defender.GeneralTestingService.Infrastructure.Steps.Interfaces;

namespace Defender.GeneralTestingService.Infrastructure.Steps;

public class LoginStep : IStep
{
    private const string StepName = "Login";

    private readonly IPortalWrapper _portalWrapper;

    public LoginStep(IPortalWrapper portalWrapper)
    {
        _portalWrapper = portalWrapper;
    }

    public async Task<TestInstance> StartAsync(TestInstance instance)
    {
        try
        {
            var session = await _portalWrapper.LoginAsync(
                await LocalSecretsHelper.GetSecretAsync(LocalSecret.Testing_Email),
                await LocalSecretsHelper.GetSecretAsync(LocalSecret.Testing_Password));

            if (instance.HttpContext?.Request?.Headers != null
                && !string.IsNullOrEmpty(session.Token))
            {
                instance.JWTToken = session.Token;
                instance.HttpContext.Request.Headers["Authorization"] = $"Bearer {session.Token}";
            }
            else
            {
                throw new Exception("Invalid token");
            }

            var highestRole = await _portalWrapper.AuthCheckAsync();

            if(highestRole == null || string.IsNullOrEmpty(highestRole)) 
            {
                throw new Exception("No roles");
            }

            instance.AddSuccessLog(StepName);
        }
        catch (Exception ex)
        {
            instance.AddFailedLog(StepName, ex.Message);
        }

        return instance;
    }
}
