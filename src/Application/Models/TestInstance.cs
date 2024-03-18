using Microsoft.AspNetCore.Http;

namespace Defender.GeneralTestingService.Application.Models;

public partial class TestInstance
{
    public record StepLog(string Step, string Message, bool IsSuccess)
    {
        public override string ToString()
        {
            var status = IsSuccess ? "passed" : "failed";

            var message = IsSuccess ? String.Empty : $"Error message: {Message}";

            return $"Step {Step} finished with status {status}. {message}";
        }
    }

    protected readonly List<StepLog> _logs = new List<StepLog>();
    public List<StepLog> GetLog => _logs;

    public void AddSuccessLog(string stepName)
    {
        var log = new StepLog(stepName, String.Empty, true);
        _logs.Add(log);
    }

    public void AddFailedLog(string stepName, string message)
    {
        var log = new StepLog(stepName, message, false);
        _logs.Add(log);
    }

    public HttpContext HttpContext { get; set; }

    public string JWTToken { get; set; }

    public static TestInstance Init(HttpContext context)
    {
        return new TestInstance()
        {
            HttpContext = context,
        };
    }

    public void PrintResult()
    {
        var failedTests = _logs.Where(x => !x.IsSuccess).ToList();

        var status = failedTests.Any() ? "failed" : "passed";

        Console.WriteLine($"Test status: {status}");

        failedTests.ForEach(x => Console.WriteLine(x));
    }
}
