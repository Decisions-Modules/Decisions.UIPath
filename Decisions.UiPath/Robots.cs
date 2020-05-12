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
    [AutoRegisterMethodsOnClass(true, "Integration/UiPath/Robots")]
    public static class Robots
    {
        /// <summary>Returns information about all robots connected to the orchestrator</summary>
        /// <param name="connection">UiPath orchestrator connection info</param>
        public static string GetRobots(UiPathOrchestratorConnection connection)
        {
            IRestResponse response = OrchAPI.GETrequest(connection, "Robots");
            return OrchAPI.ParseJson(response, "value");
        }

        /// <summary>Returns status of the robot</summary>
        /// <param name="connection">UiPath orchestrator connection info</param>
        /// <param name="robotName">Name of the robot</param>
        public static string GetRobotStatus(UiPathOrchestratorConnection connection, string robotName)
        {
            IRestResponse response = OrchAPI.GETrequest(connection, "Sessions?$expand=Robot");
            JObject jsonObj = JObject.Parse(response.Content);
            JArray jsonArr = JArray.Parse(jsonObj.SelectToken("value").ToString());
            foreach (var item in jsonArr)
            {
                if (item.SelectToken("Robot").SelectToken("Name").ToString().ToLower() == robotName.ToLower())
                {
                    return item.SelectToken("State").ToString();
                }

            }
            return "Robot not found";
        }
    }
}
