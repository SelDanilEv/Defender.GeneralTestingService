﻿using Defender.GeneralTestingService.Application.Helpers.LocalSecretHelper;
using Defender.GeneralTestingService.Application.Models;
using Defender.GeneralTestingService.Application.Steps.Interfaces;
using Defender.GeneralTestingService.Application.Clients.Portal;

namespace Defender.GeneralTestingService.Application.Steps;

public class LoginStep(IPortalWrapper portalWrapper) : IStep
{
    private const string StepName = "Login";

    public async Task<TestInstance> StartAsync(TestInstance instance)
    {
        try
        {
            var session = await portalWrapper.LoginAsync(
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

            var highestRole = await portalWrapper.AuthCheckAsync();

            if (highestRole == null || string.IsNullOrEmpty(highestRole))
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
