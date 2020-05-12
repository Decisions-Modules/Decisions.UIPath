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

namespace OrchestratorAPI
{
    public class OrchAPI
    {
        //AUTHENTICATION
        private static string Authenticate(UiPathOrchestratorConnection connection)
        {
            var client = new RestClient(connection.URL + "api/Account/Authenticate");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\n\t\"tenancyName\": \"" + connection.Tenant + "\",\n\t\"usernameOrEmailAddress\": \"" + connection.UserName + "\",\n\t\"password\": \"" + connection.Password + "\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return ParseJson(response, "result");
        }

        // HELPER METHODS
        internal static IRestResponse GETrequest(UiPathOrchestratorConnection connection, string urlPart)
        {
            string token = Authenticate(connection);
            var client = new RestClient(connection.URL + "odata/" + urlPart);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token);
            IRestResponse response = client.Execute(request);
            return response;
        }
        
        internal static IRestResponse POSTrequest(UiPathOrchestratorConnection connection, string urlPart, string parameter)
        {
            string token = Authenticate(connection);
            var client = new RestClient(connection.URL + "odata/" + urlPart);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddParameter("application / json", parameter, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return response;
        }

        internal static IRestResponse DELETErequest(UiPathOrchestratorConnection connection, string urlPart, string ID)
        {
            string token = Authenticate(connection);
            var client = new RestClient(connection.URL + "odata/" + urlPart + "(" + ID + ")");
            client.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token);
            IRestResponse response = client.Execute(request);
            return response;
        }
        
        internal static string ParseJson(IRestResponse response, string name)
        {
            if ((int)response.StatusCode == 200)
            {
                JObject jsonObj = JObject.Parse(response.Content);
                string result = jsonObj.SelectToken(name).ToString();
                return result;
            }
            return response.Content;
        }

        internal static bool GetStatus(IRestResponse response)
        {
            if ((int)response.StatusCode == 201 || (int)response.StatusCode == 204 || (int)response.StatusCode == 200)
            {
                return true;
            }
            return false;
        }

    }

    
}

