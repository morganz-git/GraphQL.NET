using RestSharp;

namespace RestSharpTestVS.Base;



// 使用 builder patten, 需要有builder class
public class RestBuilder
{
    private readonly IRestLibrary _restLibrary;

    public RestBuilder(IRestLibrary restLibrary)
    {
        _restLibrary = restLibrary;
    }

    private RestRequest RestRunRequest { get; set; } = null!;
};

