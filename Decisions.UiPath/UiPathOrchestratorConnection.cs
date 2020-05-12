using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace OrchestratorAPI
{
    [DataContract]
    public class UiPathOrchestratorConnection
    {
        [DataMember]
        public string URL { get; set; }
        [DataMember]
        public string Tenant { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }

        public UiPathOrchestratorConnection(string URL, string tenant, string userName, string password)
        {

            this.URL = URL;
            this.Tenant = tenant;
            this.UserName = userName;
            this.Password = password;
        }
    }
}
