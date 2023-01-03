using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace DurableFunction
{
    /// <summary>
    /// Create your first durable function in C#
    /// https://learn.microsoft.com/en-us/azure/azure-functions/durable/durable-functions-create-first-csharp?WT.mc_id=thomasmaurer-blog-thmaure&pivots=code-editor-vscode
    /// https://<functionappname>.azurewebsites.net/api/HelloOrchestration_HttpStart
    /// </summary>
    public static class DurableFuncExample
    {
        [FunctionName("HelloOrchestration_HttpStart")]
        public static async Task<HttpResponseMessage> HttpStart(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestMessage req,
           [DurableClient] IDurableOrchestrationClient starter,
           ILogger log)
        {
            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync("HelloOrchestration", null);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }

        [FunctionName("HelloOrchestration")]
        public static async Task<List<string>> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var outputs = new List<string>();

            // Replace "hello" with the name of your Durable Activity Function.
            outputs.Add(await context.CallActivityAsync<string>(nameof(SayHello), "Tokyo"));
            outputs.Add(await context.CallActivityAsync<string>(nameof(SayHello), "Seattle"));
            outputs.Add(await context.CallActivityAsync<string>(nameof(SayHello), "London"));

            // returns ["Hello Tokyo!", "Hello Seattle!", "Hello London!"]
            return outputs;
        }

        [FunctionName(nameof(SayHello))]
        public static string SayHello([ActivityTrigger] string name, ILogger log)
        {
            log.LogInformation($"Saying hello to {name}.");
            return $"Hello {name}!";
        }

       
    }
}