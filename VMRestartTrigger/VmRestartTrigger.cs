using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace VMRestartTrigger
{
    public static class VmRestartTrigger
    {
        [FunctionName("VmRestartTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            log.LogInformation(requestBody);
            var data = JsonConvert.DeserializeObject<Rootobject>(requestBody);//Can save data to DB

            return new OkObjectResult(data);
        }
    }

    public class Rootobject
    {
        public string schemaId { get; set; }
        public string data { get; set; }
    }

    public class Data
    {
        public Essentials essentials { get; set; }
        public Alertcontext alertContext { get; set; }
    }

    public class Essentials
    {
        public string alertId { get; set; }
        public string alertRule { get; set; }
        public string severity { get; set; }
        public string signalType { get; set; }
        public string monitorCondition { get; set; }
        public string monitoringService { get; set; }
        public string[] alertTargetIDs { get; set; }
        public string[] configurationItems { get; set; }
        public string originAlertId { get; set; }
        public DateTime firedDateTime { get; set; }
        public string description { get; set; }
        public string essentialsVersion { get; set; }
        public string alertContextVersion { get; set; }
    }

    public class Alertcontext
    {
        public Authorization authorization { get; set; }
        public string channels { get; set; }
        public string claims { get; set; }
        public string caller { get; set; }
        public string correlationId { get; set; }
        public string eventSource { get; set; }
        public DateTime eventTimestamp { get; set; }
        public string httpRequest { get; set; }
        public string eventDataId { get; set; }
        public string level { get; set; }
        public string operationName { get; set; }
        public string operationId { get; set; }
        public Properties properties { get; set; }
        public string status { get; set; }
        public string subStatus { get; set; }
        public DateTime submissionTimestamp { get; set; }
        public string ResourceType { get; set; }
        public string ActivityLogEventDescription { get; set; }
    }

    public class Authorization
    {
        public string action { get; set; }
        public string scope { get; set; }
    }

    public class Properties
    {
        public string eventCategory { get; set; }
        public string entity { get; set; }
        public string message { get; set; }
        public string hierarchy { get; set; }
    }
}
