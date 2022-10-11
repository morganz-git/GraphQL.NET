using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using RestSharp;

namespace RestSharpTestVS.Base;

public interface IRestLibrary
{
    //默认是一个public abstract的属性 - 焗油get set 方法, 接口中不能放具体的类，只能放抽象的类
    RestClient RestClient { get; set; }
}

//这个是一个接口的实现类，用来实现接口中的方法
public class RestLibrary : IRestLibrary
{
    // C# 属性和字段的区别--https://www.shuzhiduo.com/A/E35ple3L5v/
    //这个是接口中的属性，下面实现用来实现接口
    public RestClient RestClient { get; set; }

    // private RestClientOptions _restClientOptions;
// 构造方法， 用来初始化RestClient
//injecting the web application factory
// 由于我么修改了这里的构造函数 - 一个具体函数, 所以在依赖注入那里也好进行修改
    public RestLibrary(WebApplicationFactory<GraphQLProductApp.Startup> webApplicationFactory)
    {
        var restClientOptions = new RestClientOptions
        {
            BaseUrl = new Uri("https://localhost:5001/"),
            // lamda 函数
            RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
        };

        //Spawn out system under test SUT
        var client = webApplicationFactory.CreateDefaultClient();
        //rest client initial 
        RestClient = new RestClient(client, restClientOptions);
        // RestClient = new RestClient(restClientOptions); // 这个用的是标准的5001端口,
        // 上面的一步做的是讲webapplictionfactory的client传递给restclient
    }
}