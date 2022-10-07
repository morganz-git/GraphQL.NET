namespace RestSharpTestVS.Base;

public interface IRestFactory
{
    IRestBuilder Create();
}

public class RestFactory : IRestFactory
{
    private readonly IRestBuilder _restBuilder;

    public RestFactory(IRestBuilder restBuilder)
    {
        _restBuilder = restBuilder;
    }

    //实现接口中的方法
    public IRestBuilder Create()
    {
        //返回builder的接口,这样可以嗲用builder的方法
        return _restBuilder;
    }
}