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
    public RestLibrary()
    {
        var restClientOptions = new RestClientOptions
        {
            BaseUrl = new Uri("https://localhost:5001/"),
            // lamda 函数
            RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
        };
        //rest client initial 
        RestClient = new RestClient(restClientOptions);
    }
}