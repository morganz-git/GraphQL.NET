using FluentAssertions;
using GraphQL;
using GraphQL.Client.Abstractions;
using GraphQLProductApp.Data;

namespace GraphQLTesting;

public class UnitTest1
{
    private readonly IGraphQLClient _graphQlClient;

    public UnitTest1(IGraphQLClient graphQlClient)
    {
        _graphQlClient = graphQlClient;
    }

    [Fact]
    public async Task Test1()
    {
        // var graphQLClient = new GraphQLHttpClient(new GraphQLHttpClientOptions
        // {
        //     EndPoint = new Uri("http://localhost:5000/graphql")
        // }, new NewtonsoftJsonSerializer());
        //Rerquest - Query
        var query = new GraphQLRequest
        {
            Query = @"{
                          products {
                            name
                            price
                            components {
                              id
                              name
                              description
                            }
                          } 
                        }"
        };
        // var response = await graphQLClient.SendQueryAsync<IEnumerable<Product>>(query);
        var response = await _graphQlClient.SendQueryAsync<ProductQueryResponse>(query);
        // response.Data.FirstOrDefault().Name.Should();
        response.Data.Products.Should().Contain(c => c.Name == "Keyboard");
    }
}

public class ProductQueryResponse
{
    public IEnumerable<Product> Products { get; set; }
}