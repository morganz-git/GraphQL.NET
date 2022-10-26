using System.Diagnostics;
using FluentAssertions;
using GraphQLProductApp.Controllers;
using GraphQLProductApp.Data;
using Newtonsoft.Json.Linq;
using RestSharp;
using TechTalk.SpecFlow.Assist;

namespace RestSharpSpecflow.Steps;

[Binding]
public sealed class BasicOperation
{
    // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

    private readonly ScenarioContext _scenarioContext;
    private RestClient _restClient;
    private Product? _response;

    public BasicOperation(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
        _restClient = _scenarioContext.Get<RestClient>("RestClient");
    }

    [Given(@"I perform a GET operation of ""(.*)""")]
    public async Task GivenIPerformAgetOperationOf(string path, Table table)
    {
        dynamic data = table.CreateDynamicInstance();
        //token
        var token = GetToken();

        /*Driver中
           // Add into ScenarioContext
            scenarioContext.Add("RestClient", restClient);*/
        // rest request
        // var request = new RestRequest("Product/GetProductById/1");
        var request = new RestRequest(path);
        request.AddUrlSegment("id", (int)data.ProductId);
        request.AddHeader("Authorization", $"Bearer {token}");
        //Perform the get request 
        _response = await _restClient.GetAsync<Product>(request);
    }

    [Given(@"I should get the product name as ""(.*)""")]
    public void GivenIShouldGetTheProductNameAs(string value)
    {
        Debug.Assert(_response != null, nameof(_response) + " != null");
        _response.Name.Should().Be(value);
    }

    public string GetToken()
    {
        //Rest Request
        var authRequest = new RestRequest("api/Authenticate/login");
        //Typed object being passed to body in request 
        authRequest.AddJsonBody(new LoginModel
        {
            UserName = "KK",
            Password = "123456"
        });
        //Perform GET operation
        var authResponse = _restClient.PostAsync(authRequest).Result.Content;
        return JObject.Parse(authResponse)["token"].ToString();
    }
}