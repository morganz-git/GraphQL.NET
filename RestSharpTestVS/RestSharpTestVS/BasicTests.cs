using System.Net;
using FluentAssertions;
using GraphQLProductApp.Controllers;
using GraphQLProductApp.Data;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharpTestVS.Base;
using Xunit;

namespace RestSharpTestVS;

// IClassFixture -> Xunit 是一个泛型接口，用于在测试类中注入一个实例，这个实例在整个测试类中都可以使用
// public class BasicTests : IClassFixture<RestLibrary>   
// 由于使用了dependence injection，所以不需要使用IClassFixture
public class BasicTests
{
    //私有变量, 这里的RestClient 
    private readonly RestClient _client;
    private readonly IRestBuilder _restBuilder;

    // var restLibrary = new RestLibrary();  这里的 IRestLibrary 是自己写的一个类，用于初始化RestClient
    public BasicTests(IRestLibrary restLibrary, IRestBuilder restBuilder)
    {
        _restBuilder = restBuilder;
        _client = restLibrary.RestClient;
        // _restBuilder = restBuilder;
    }

    [Fact]
    // Task : Represents an asynchronous operation.
    public async Task Test1()
    {
        // _restBuilder.WithRequest("heheh").Withheader().WithRequest().Withheader();
        //Rest Request initialization
        var request = new RestRequest("Product/GetProductById/1");
        request.AddHeader("authorization", $"Bearer {GetToken()}");
        //perform GET operation
        // 因为是async 所以需要 await在这里, 然后 这个方法需要用async修饰
        /*加入的泛型, 说明get返回的是一个product 类型*/
        Product? response = await _client.GetAsync<Product>(request);
        //Assert
        // 安准fluent assertion 来进行assert的操作
        // 由于product 是一个类, 所以可以列出其各种变量
        response?.Name.Should().Be("Keyboard");
    }

    [Fact]
    // Task : Represents an asynchronous operation.
    public async Task GetWithQuerySegmentTest()
    {
        //Rest Request initialization
        var request = new RestRequest("Product/GetProductById/{id}");
        request.AddHeader("authorization", $"Bearer {GetToken()}");
        // 这里是用segment 来传递参数的
        request.AddUrlSegment("id", 2);
        //perform GET operation
        // 因为是async 所以需要 await在这里, 然后 这个方法需要用async修饰
        /*加入的泛型, 说明get返回的是一个product 类型*/
        Product? response = await _client.GetAsync<Product>(request);
        //Assert
        // 安准fluent assertion 来进行assert的操作
        // 由于product 是一个类, 所以可以列出其各种变量
        response?.Price.Should().Be(400);
    }

    [Fact]
    // Task : Represents an asynchronous operation.
    public async Task GetWithQueryParameterTest()
    {
        //Rest Request initialization   GetProductByIdAndName?Id=1&Name=1
        var request = new RestRequest("Product/GetProductByIdAndName");
        request.AddHeader("authorization", $"Bearer {GetToken()}");
        request?.AddQueryParameter("id", "2");
        request?.AddQueryParameter("name", "Monitor");
        //perform GET operation
        // 因为是async 所以需要 await在这里, 然后 这个方法需要用async修饰
        /*加入的泛型, 说明get返回的是一个product 类型*/
        if (request != null)
        {
            Product? response = await _client.GetAsync<Product>(request);
            //Assert
            // 安准fluent assertion 来进行assert的操作
            // 由于product 是一个类, 所以可以列出其各种变量
            // response?.ProductType.Should().Be(1);
            response?.ProductType.Should().Be(ProductType.MONITOR);
        }
    }

    [Fact]
    // Task : Represents an asynchronous operation.
    public async Task PostProductTest()
    {
        //Rest Request initialization   GetProductByIdAndName?Id=1&Name=1
        var request = new RestRequest("Product/Create");
        request.AddHeader("authorization", $"Bearer {GetToken()}");
        request.AddJsonBody(new Product
        {
            Name = "Cabinet",
            Description = "Gaming Cabinet",
            Price = 300,
            ProductType = ProductType.PERIPHARALS
        });
        // request?.AddQueryParameter("id", "2");
        // request?.AddQueryParameter("name", "Monitor");
        //perform GET operation
        // 因为是async 所以需要 await在这里, 然后 这个方法需要用async修饰
        /*加入的泛型, 说明get返回的是一个product 类型*/
        var response = await _client.PostAsync<Product>(request);
        //Assert
        // 安准fluent assertion 来进行assert的操作
        // 由于product 是一个类, 所以可以列出其各种变量
        // response?.ProductType.Should().Be(1);
        response?.Price.Should().Be(300);
    }

    [Fact]
    // Task : Represents an asynchronous operation.
    public async Task FileUpLoadTest()
    {
        //Rest Request initialization   GetProductByIdAndName?Id=1&Name=1
        // 在这里就直接说明了POST方法
        var request = new RestRequest("Product", Method.Post);
        request.AddHeader("authorization", $"Bearer {GetToken()}");
        // 这里的myFile 是应用程序中写死的,所以用了这个
        request.AddFile("myFile",
            @"F:\OneDrive\OneDrive - Morgan\OneDrive\Morgan\Study\Video\C#\Api Testing\AppWithoutAuth\Pictures\0f5985669aaa035e5f2594e7e75b064.jpg",
            "multipart/form-data");
        var response = await _client.ExecuteAsync(request);
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    // ? 表示这种返回值可以是null
    private JToken? GetToken()
    {
        var authRequest = new RestRequest("api/Authenticate/Login", Method.Post);

        authRequest.AddJsonBody(new LoginModel
        {
            UserName = "admin",
            Password = "123456"
        });
        var response = _client.PostAsync(authRequest).Result.Content;

        return JObject.Parse(response)["token"];

        // var authResponse = _client.PostAsync(authRequest).Result.Content;
    }
}