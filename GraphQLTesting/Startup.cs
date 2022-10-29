using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLTesting;

public class Startup
{
    public void ConfigureService(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IGraphQLClient>(new GraphQLHttpClient(
            new Uri("http://localhost:5000/graphql"),
            new NewtonsoftJsonSerializer())
        );
    }
}