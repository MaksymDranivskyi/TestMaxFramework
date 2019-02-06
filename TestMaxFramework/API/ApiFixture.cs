using Newtonsoft.Json.Linq;
using RestSharp;

namespace NUnitTestProject1.Tests
{
    public class ApiFixture
    {
        private string BaseUrl { get; set; }
        public static RestClient Client { get; set; }
        public RestRequest Request { get; set; }
        public IRestResponse Response { get; set; }
        public JObject ResponseJson { get; set; }
        private void SetUp()
        {
            BaseUrl = "https://jsonplaceholder.typicode.com/";
        }
        public void Initialize()
        {
            SetUp();
            Client = new RestClient
            {
                BaseUrl = new System.Uri(BaseUrl),
                Timeout = System.Convert.ToInt32(System.TimeSpan.FromMinutes(3).TotalMilliseconds)
            };
        }
    }
}
