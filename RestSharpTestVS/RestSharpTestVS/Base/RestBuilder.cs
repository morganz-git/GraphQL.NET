using RestSharp;

namespace RestSharpTestVS.Base;

// 使用 builder patten, 需要有builder class
/*public interface IRestBuilder
{
    RestRequest RestRequest { get; set; }

    // 需要声明, 以便于BasicTest 中的测试用例进行调佣
    public IRestBuilder WithRequest(string request);
     public IRestBuilder Withheader(string name, string value);
     public IRestBuilder WithQueryParameter(string name, string value);
     public IRestBuilder WithUrlSegment(string name, string value);
     public IRestBuilder WithBody(object body);
 }*/

public interface IRestBuilder
{
    RestRequest RestRequest { get; set; }
    IRestBuilder WithRequest(string request);
    IRestBuilder WithHeader(string name, string value);
    IRestBuilder WithQueryParameter(string name, string value);
    IRestBuilder WithUrlSegment(string name, string value);
    IRestBuilder WithBody(object body);
    Task<T?> WithGet<T>() where T : new();
    Task<T?> WithPost<T>() where T : new();
    Task<RestResponse> WithPost();
    Task<T?> WithPut<T>() where T : new();
    Task<T?> WithDelete<T>() where T : new();
    IRestBuilder WithFile(string file, string path, string contentType);
     Task<RestResponse> WithExecuteAsync();
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
    public IRestBuilder WithHeader(string name, string value)
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

    //    void WithFile(string file, string path);

    public IRestBuilder WithFile(string file, string path, string contentType)
    {
        RestRequest.AddFile(file, path, contentType);
        return this;
    }

    // Get Put...等方法, 返回的是一个泛型的类型 -- _client.GetAsync<Product>(request);
    // 至于await async 相关的东西 Test方法由于被async修饰，表明这个可以是异步方法，方法内部await修饰的Task，执行到这一行代码时，会等待Task执行完成后，才会执行Test方法里下一行的代码。
    //--https://blog.csdn.net/weixin_49431316/article/details/111901457
    /* ? 表示返回不能为空   async 的三大返回类型
     返回类型  - Task<TResult> 
    返回类型 - Task
        返回类型 - void
        https://www.cnblogs.com/liqingwen/p/6218994.html
        */
    public async Task<T?> WithGet<T>() where T : new()
    {
        return await _restLibrary.RestClient.GetAsync<T>(RestRequest);
    }

    public async Task<T?> WithPost<T>() where T : new()
    {
        return await _restLibrary.RestClient.PostAsync<T>(RestRequest);
    }

    public async Task<RestResponse> WithPost()
    {
        return await _restLibrary.RestClient.PostAsync(RestRequest);
    }

    public async Task<RestResponse> WithExecuteAsync()
    {
        return await _restLibrary.RestClient.ExecuteAsync(RestRequest);
    }

    public async Task<T?> WithPut<T>() where T : new()
    {
        return await _restLibrary.RestClient.PutAsync<T>(RestRequest);
    }

    public async Task<T?> WithDelete<T>() where T : new()
    {
        return await _restLibrary.RestClient.DeleteAsync<T>(RestRequest);
    }
};