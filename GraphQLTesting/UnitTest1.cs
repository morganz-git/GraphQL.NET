using System.Net.Http.Headers;
using FluentAssertions;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQLProductApp.Data;

namespace GraphQLTesting;

public class UnitTest1
{
    [Fact] 
    public async Task  Test1()
    {
        var graphQLClient = new GraphQLHttpClient(new GraphQLHttpClientOptions
        {
            EndPoint = new Uri("http://localhost:5000/graphql")
        }, new NewtonsoftJsonSerializer());
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
        var response = await graphQLClient.SendQueryAsync<ProductQueryResponse>(query);
        // response.Data.FirstOrDefault().Name.Should();
        response.Data.Products.Should().Contain(c => c.Name == "Keyboard");
    }
}

public class ProductQueryResponse
{
    public  IEnumerable<Product> Products { get; set; }
}