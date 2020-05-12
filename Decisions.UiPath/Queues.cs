using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DecisionsFramework.Design.Flow;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace OrchestratorAPI
{
    [AutoRegisterMethodsOnClass(true, "Integration/UiPath/Queues")]
    public static class Queues
    {
        /// <summary>Returns information about all QUeues in the orchestrator</summary>
        /// <param name="connection">UiPath orchestrator connection info</param>
        public static string GetQueues(UiPathOrchestratorConnection connection)
        {
            IRestResponse response = OrchAPI.GETrequest(connection, "QueueDefinitions");
            return OrchAPI.ParseJson(response, "value");
        }

        /// <summary>Creates new queue in orchestrator. Returns bool if created successfuly</summary>
        /// <param name="connection">UiPath orchestrator connection info</param>
        /// <param name="parameters">Name, Description, Number of retries, Automatic retry("true"/"false"), Unique reference("true"/"false")</param>
        public static bool CreateQueue(UiPathOrchestratorConnection connection, string[] parameters)
        {
            IRestResponse response = OrchAPI.POSTrequest(connection, "QueueDefinitions", "{\r\n  \"Name\": \"" + parameters[0] + "\",\r\n  \"Description\": \"" + parameters[1] + "\",\r\n  \"MaxNumberOfRetries\": " + parameters[2] + ",\r\n  \"AcceptAutomaticallyRetry\": " + parameters[3] + ",\r\n  \"EnforceUniqueReference\": " + parameters[4] + "\r\n}");
            return OrchAPI.GetStatus(response);
        }

        /// <summary>Deletes queue in orchestrator. Returns bool if  delete was successful</summary>
        /// <param name="connection">UiPath orchestrator connection info</param>
        /// <param name="ID">ID of the queue</param>
        public static bool DeleteQueue(UiPathOrchestratorConnection connection, string ID)
        {
            IRestResponse response = OrchAPI.DELETErequest(connection, "QueueDefinitions", ID);
            return OrchAPI.GetStatus(response);
        }
    }
}
