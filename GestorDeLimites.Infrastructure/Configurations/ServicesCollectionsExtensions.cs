using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.Extensions.DependencyInjection;
using GestorDeLimites.Domain.Repositories;
using GestorDeLimites.Infrastructure.Data;
using GestorDeLimites.Application.Interfaces;
using GestorDeLimites.Application.Services;
using GestorDeLimites.Application.Mapping;

namespace GestorDeLimites.Infrastructure.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            services.AddSingleton<IAmazonDynamoDB>(provider =>
                new AmazonDynamoDBClient(new AmazonDynamoDBConfig
                {
                    RegionEndpoint = Amazon.RegionEndpoint.USEast2 
                }));

            services.AddSingleton<IDynamoDBContext>(provider =>
            {
                var dynamoDBClient = provider.GetRequiredService<IAmazonDynamoDB>();
                return new DynamoDBContext(dynamoDBClient);
            });

            services.AddScoped<ILimiteClienteRepository, LimiteClienteRepository>();
            services.AddScoped<ILimiteClienteService, LimiteClienteService>();
        }
    }
}
