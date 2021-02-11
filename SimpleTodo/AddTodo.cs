using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SimpleTodo
{
    public static class AddTodo
    {
        [FunctionName("AddTodo")]

        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string todo = req.Query["todo"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            todo = todo ?? data?.todo;

            string responseMessage = "";
            IActionResult response;

            if (!string.IsNullOrEmpty(todo))
            {
                
                responseMessage = $"The todo was added. This HTTP triggered function executed successfully.";
                response = new OkObjectResult(responseMessage);
            } else
            {
                response = new BadRequestObjectResult("missing todo param");
            }

            return response;

        }
    }
}
