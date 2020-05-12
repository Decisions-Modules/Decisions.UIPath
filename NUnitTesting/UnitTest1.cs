using NUnit.Framework;
using OrchestratorAPI;

namespace Tests
{
    public class Tests
    {
        private UiPathOrchestratorConnection connection = new UiPathOrchestratorConnection("URL", "Tenant", "Username", "Password");

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void GetUsers()
        {
            Assert.Pass(Users.GetUsers(connection));
        }
        
        [Test]
        public void CreateUser()
        {
            Assert.True(Users.CreateUser(connection, new string[] { "Name", "Surname", "1TEST" }, new string[] { "Robot", "Developer" }));
        }

        [Test]
        public void DeleteUser()
        {
            Assert.True(Users.DeleteUser(connection, "75"));
        }

        [Test]
        public void deActivateUser()
        {
            Assert.True(Users.deActivateUser(connection, "74", "true"));
        }

        [Test]
        public void GetRobots()
        {
            Assert.Pass(Robots.GetRobots(connection));
        }

        [Test]
        public void GetRobotStatus()
        {
            Assert.Pass(Robots.GetRobotStatus(connection, "D02-marek"));
        }

        [Test]
        public void GetRoles()
        {
            Assert.Pass(Users.GetRoles(connection));
        }

        [Test]
        public void DeleteRole()
        {
            Assert.True(Users.DeleteRole(connection, "7"));
        }

        [Test]
        public void GetJobs()
        {
            Assert.Pass(Jobs.GetJobs(connection));
        }

        [Test]
        public void GetJobStatus()
        {
            Assert.Pass(Jobs.GetJobStatus(connection, "1"));
        }

        [Test]
        public void StartJob()
        {
            Assert.Pass(Jobs.StartJob(connection, new string[] { "90d2c205-4a7a-4a32-8da8-31dc8c904993", "15" }, new string[] { "Subject-Sub", "Body-Bod" }));
        }

        [Test]
        public void CancelJob()
        {
            Assert.True(Jobs.CancelJob(connection, "60996", "Kill"));
        }

        [Test]
        public void GetQueues()
        {
            Assert.Pass(Queues.GetQueues(connection));
        }

        [Test]
        public void CreateQueue()
        {
            Assert.True(Queues.CreateQueue(connection, new string[] { "ATEST1", "TEST123", "0", "false", "false" }));
        }

        [Test]
        public void DeleteQueue()
        {
            Assert.True(Queues.DeleteQueue(connection,"55"));
        }
        
    }
}