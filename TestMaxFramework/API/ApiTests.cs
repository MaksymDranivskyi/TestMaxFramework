using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Linq;
using System.Net;

namespace NUnitTestProject1.Tests
{
    public class ApiTests : ApiFixture
    {

        [Test]
        [Author("Maksym Dramivskyi")]
        [Category("API test")]
        public void ApiGet()
        {
            Initialize();
            Request = new RestRequest("/comments/1", Method.GET);
            Response = Client.Get(Request);
            Assert.That(Response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            ResponseJson = JObject.Parse(Response.Content);
            string email = (string)ResponseJson.SelectTokens("email").FirstOrDefault();
            StringAssert.AreEqualIgnoringCase(email, "Eliseo@gardner.biz");
        }

        [Test]
        [Author("Maksym Dramivskyi")]
        [Category("API test")]
        public void ApiDelete()
        {
            Initialize();
            Request = new RestRequest("/posts/1", Method.DELETE);
            Response = Client.Execute(Request);
            Assert.That(Response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        }
        [Test]
        [Author("Maksym Dramivskyi")]
        [Category("API test")]
        public void ApiPost()
        {

            Initialize();
            Request = new RestRequest("/posts", Method.POST);
            Request.RequestFormat = DataFormat.Json;
            Request.AddParameter("email","Lura@rod.tv");
            Response = Client.Execute(Request);
            Assert.That(Response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            ResponseJson = JObject.Parse(Response.Content);
            string id = (string)ResponseJson.SelectTokens("id").FirstOrDefault();
            StringAssert.AreEqualIgnoringCase(id, "101");

        }

        

        

    }
}
