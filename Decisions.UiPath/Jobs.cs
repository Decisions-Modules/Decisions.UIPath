using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RestSharp;
using DecisionsFramework.Design.Flow;

namespace OrchestratorAPI
{
    [AutoRegisterMethodsOnClass(true, "Integration/UiPath/Jobs")]
    public static class Jobs
    {
        /// <summary>Returns information about all jobs in the orchestrator</summary>
        /// <param name="connection">UiPath orchestrator connection info</param>
        public static string GetJobs(UiPathOrchestratorConnection connection)
        {
            IRestResponse response = OrchAPI.GETrequest(connection, "Jobs");
            return OrchAPI.ParseJson(response, "value");
        }

        /// <summary>Returns information about status of the job</summary>
        /// <param name="connection">UiPath orchestrator connection info</param>
        /// <param name="ID">ID of the job</param>
        public static string GetJobStatus(UiPathOrchestratorConnection connection, string ID)
        {
            IRestResponse response = OrchAPI.GETrequest(connection, "Jobs(" + ID + ")");
            return OrchAPI.ParseJson(response, "State");
        }

        /// <summary>Stops or kills job. Returns bool if it was successful</summary>
        /// <param name="connection">UiPath orchestrator connection info</param>
        /// <param name="ID">ID of the process</param>
        /// <param name="strategy">Input "Softstop" to stop process, "Kill" to kill it</param>
        public static bool CancelJob(UiPathOrchestratorConnection connection, string ID, string strategy)
        {
            IRestResponse response = OrchAPI.POSTrequest(connection, "Jobs(" + ID + ")/UiPath.Server.Configuration.OData.StopJob", "{\"strategy\": \"" + strategy + "\"}");
            return OrchAPI.GetStatus(response);
        }

        /// <summary>Starts job with input parameters</summary>
        /// <param name="connection">UiPath orchestrator connection info</param>
        /// <param name="JobInfo">Package release key, Robot ID</param>
        /// <param name="inputParameters">Input parameters in format {input key}-{value}. Input key is case sensitive</param>
        public static string StartJob(UiPathOrchestratorConnection connection, string[] jobInfo, string[] inputParameters)
        {
            string parameters = "";
            foreach (string par in inputParameters)
            {
                string[] splitter = par.Split('-');
                parameters = parameters + "\\\"" + splitter[0] + "\\\":\\\"" + splitter[1] + "\\\",";
            }
            parameters = parameters.TrimEnd(',');
            IRestResponse response = OrchAPI.POSTrequest(connection, "Jobs/UiPath.Server.Configuration.OData.StartJobs", "{\"startInfo\":{\"ReleaseKey\":\"" + jobInfo[0] + "\",\"RobotIds\":[" + jobInfo[1] + "],\"JobsCount\":0,\"Strategy\":\"Specific\",\"InputArguments\":\"{" + parameters + "}\"}}");
            return OrchAPI.ParseJson(response, "value");
        }

    }
}
