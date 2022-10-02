using RestSharp;

namespace RestSharpTestVS.Base;

// 使用 builder patten, 需要有builder class
public interface IRestBuilder
{
    RestRequest RestRequest { get; set; }

    // 需要声明, 以便于BasicTest 中的测试用例进行调佣
    public IRestBuilder WithRequest(string request);
    public IRestBuilder Withheader(string name, string value);
    public IRestBuilder WithQueryParameter(string name, string value);
    public IRestBuilder WithUrlSegment(string name, string value);
    public IRestBuilder WithBody(object body);
}

public class RestBuilder : IRestBuilder
{
    // 用 RestClient 
    private readonly IRestLibrary _restLibrary;
    //..

    public RestBuilder(IRestLibrary restLibrary)
    {
        _restLibrary = restLibrary;
    }

    public RestRequest RestRequest { get; set; } = null!;

/*return this?
this 一般指的是对象本身,如果你是在Form里面写的话那返回的就是一个窗体对象
方法的格式如下
private Form getForm(参数)
{
//方法体
....
//最后
return this;//表示返回窗体对象
}

至于什么时候用的话,当你的方法需要返回一个对象的时候才用Return
也可以用 return;来退出方法,看你自己的理解了*/
    public IRestBuilder WithRequest(string request)
    {
        RestRequest = new RestRequest(request);
        // this 就是这个函数所在类的对象 = new RestBuilder
        return this;
    }

    // Add header 是在request下面 
    /*        request.AddHeader("authorization", $"Bearer {GetToken()}");*/
    public IRestBuilder Withheader(string name, string value)
    {
        RestRequest.AddHeader(name, value);
        return this;
    }

    public IRestBuilder WithQueryParameter(string name, string value)
    {
        RestRequest.AddQueryParameter(name, value);
        return this;
    }

    public IRestBuilder WithUrlSegment(string name, string value)
    {
        RestRequest.AddUrlSegment(name, value);
        return this;
    }

    public IRestBuilder WithBody(object body)
    {
        RestRequest.AddBody(body);
        return this;
    }
};