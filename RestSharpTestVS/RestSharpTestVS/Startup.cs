using Microsoft.Extensions.DependencyInjection;
using RestSharpTestVS.Base;

namespace RestSharpTestVS;

//Program.cs 和 Startup.cs 各自作用及启动顺序
// https://www.programminghunter.com/article/9025938164/
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IRestLibrary, RestLibrary>();
    }
} 