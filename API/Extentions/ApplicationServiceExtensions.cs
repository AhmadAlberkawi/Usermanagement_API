using API.Helpers;
using API.Services;
using DataAccess.Data;
using DataAccess.DbAccess;
using Microsoft.OpenApi.Models;

namespace API.Extentions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
            services.AddSingleton<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            //services.AddSingleton(x => new BlobServiceClient(config.GetValue<string>("AzureBlobStorageConnectionString")));
            //services.AddSingleton<IFileService, FileService>();

            return services;
        }
    }
}
