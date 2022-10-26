using System;
using System.Security.Policy;
using RestSharp;

namespace RestSharpSpecflow.Drivers
{
    public class Driver
    {
        //context injection
        // public RestClient RestClient { get; set; }
        public Driver(ScenarioContext scenarioContext)
        {
            var restClientOptions = new RestClientOptions
            {
                BaseUrl = new Uri("https://localhost:5001"),
                RemoteCertificateValidationCallback = (sender, certificate, chain, errors) => true
            };
            // Rest Client
            RestClient restClient = new RestClient(restClientOptions);
            
            // Add into ScenarioContext
            scenarioContext.Add("RestClient", restClient);
        }
    }
}