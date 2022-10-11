using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using RestSharpTestVS.Base;

namespace RestSharpTestVS;

//Program.cs 和 Startup.cs 各自作用及启动顺序
// https://www.programminghunter.com/article/9025938164/
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        /*services.AddSingleton<IRestLibrary, RestLibrary>();
        services.AddScoped<IRestBuilder, RestBuilder>();
        services.AddScoped<IRestFactory, IRestFactory>();*/
        services
            // .AddSingleton<IRestLibrary, RestLibrary>()  // 因为在restlibrary 中的构造函数已经发生了修改,所以这里也要进行修改
            .AddSingleton<IRestLibrary>(new RestLibrary(new WebApplicationFactory<GraphQLProductApp.Startup>())) 
            .AddScoped<IRestBuilder, RestBuilder>()
            .AddScoped<IRestFactory, RestFactory>();
    }
}