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

    [AutoRegisterMethodsOnClass(true, "Integration/UiPath/Users")]
    public static class Users
    {

        /// <summary>Returns information about all users in the orchestrator</summary>
        /// <param name="connection">UiPath orchestrator connection info</param>
        public static string GetUsers(UiPathOrchestratorConnection connection)
        {
            IRestResponse response = OrchAPI.GETrequest(connection, "Users");
            return OrchAPI.ParseJson(response, "value");
        }

        /// <summary>Creates new user in orchestrator. Returns information about new created user</summary>
        /// <param name="connection">UiPath orchestrator connection info</param>
        /// <param name="parameters">Name(optional), Surname(optinal), Username(required). Optional can be empty string "" </param>
        /// <param name="roles">Roles that will be assigned to user</param>
        public static bool CreateUser(UiPathOrchestratorConnection connection, string[] parameters, string[] roles)
        {
            string rolesToJson = "";
            foreach (string role in roles)
            {
                rolesToJson = rolesToJson + "\"" + role + "\",";
            }
            rolesToJson = rolesToJson.TrimEnd(Convert.ToChar(","));
            IRestResponse response = OrchAPI.POSTrequest(connection, "Users", "{\"Name\":\"" + parameters[0] + "\",\"Surname\":\"" + parameters[1] + "\",\"UserName\":\"" + parameters[2] + "\",\"TenancyName\":\"" + connection.Tenant + "\",\"RolesList\":[" + rolesToJson + "]}");
            return OrchAPI.GetStatus(response);
        }

        /// <summary>Deletes user in orchestrator. Returns bool if  delete was successful</summary>
        /// <param name="connection">UiPath orchestrator connection info</param>
        /// <param name="ID">ID of the user</param>
        public static bool DeleteUser(UiPathOrchestratorConnection connection, string ID)
        {
            IRestResponse response = OrchAPI.DELETErequest(connection, "Users", ID);
            return OrchAPI.GetStatus(response);
        }

        /// <summary>Activates or deactivates user in orchestrator. Returns bool if it was successful</summary>
        /// <param name="connection">UiPath orchestrator connection info</param>
        /// <param name="ID">ID of the user</param>
        /// <param name="activate">True/False as string</param>
        public static bool deActivateUser(UiPathOrchestratorConnection connection, string ID, string activate)
        {
            IRestResponse response = OrchAPI.POSTrequest(connection, "Users(" + ID + ")/UiPath.Server.Configuration.OData.SetActive", "{\"active\":" + activate + "}");
            return OrchAPI.GetStatus(response);
        }

        /// <summary>Returns information about all roles in the orchestrator</summary>
        /// <param name="connection">UiPath orchestrator connection info</param>
        public static string GetRoles(UiPathOrchestratorConnection connection)
        {
            IRestResponse response = OrchAPI.GETrequest(connection, "Roles?$expand=Permissions");
            return OrchAPI.ParseJson(response, "value");
        }

        /// <summary>Deletes role in orchestrator. Returns bool if  delete was successful</summary>
        /// <param name="connection">UiPath orchestrator connection info</param>
        /// <param name="ID">ID of the role</param>
        public static bool DeleteRole(UiPathOrchestratorConnection connection, string ID)
        {
            IRestResponse response = OrchAPI.DELETErequest(connection, "Roles", ID);
            return OrchAPI.GetStatus(response);
        }

    }
}
