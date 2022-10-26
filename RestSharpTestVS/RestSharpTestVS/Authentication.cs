using FluentAssertions;
using GraphQLProductApp.Controllers;
using GraphQLProductApp.Data;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using RestSharp;
using Xunit;

namespace RestSharpTestVS;

public class Authentication
{
    private RestClientOptions _restClientOptions;

    public Authentication()
    {
        // 设置restClient 的选项
        _restClientOptions = new RestClientOptions
        {
            BaseUrl = new Uri("https://localhost:5001/"),
            // lamda 函数
            RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
        };
    }

    [Fact(Skip = "skip it")]
    // Task : Represents an asynchronous operation.
    public async Task GetWithQueryParameterTest()
    {
        //Rest Client initialization
        var client = new RestClient(_restClientOptions);
        //Rest Request initialization   GetProductByIdAndName?Id=1&Name=1
        var authRequest = new RestRequest("api/Authenticate/Login", Method.Post);
        //anonymous object being passed as a parameter
        /*request.AddJsonBody(new
        {
            username = "kk",
            password = "123456"
        });*/

        // Types object being passed as a parameter
        authRequest.AddJsonBody(new LoginModel()
        {
            UserName = "kk",
            Password = "123456"
        });
        // return a json body, and get the content from response 
        var authResponse = client.PostAsync(authRequest).Result.Content;
        //get the token from response
        var token = JObject.Parse(authResponse ?? throw new InvalidOperationException())["token"];
        //Rest Request initialization
        var productGetResponse = new RestRequest("Product/GetProductById/1");
        productGetResponse.AddHeader("Authorization", $"Bearer {token}");
        //perform GET operation
        // 因为是async 所以需要 await在这里, 然后 这个方法需要用async修饰
        /*加入的泛型, 说明get返回的是一个product 类型*/
        Product? productResponse = await client.GetAsync<Product>(productGetResponse);
        //Assert
        // 安准fluent assertion 来进行assert的操作
        // 由于product 是一个类, 所以可以列出其各种变量
        productResponse?.Name.Should().Be("Keyboard");
    }
}