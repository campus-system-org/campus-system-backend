using Campus_System_Database_Model;
using Campus_System_WebApi.Processors;
using Campus_System_WebApi.Services;
using Campus_System_WebApi.Swaggers;
using Microsoft.OpenApi.Models;
using MongoDB.Bson.Serialization;
using System.Reflection;

namespace Campus_System_WebApi.Extensions
{
    public static class WebApplicationBuilderExtension
    {
        public static IServiceCollection Add_Dependency_Injection(this IServiceCollection service, IConfiguration configuration)
        {
            //service
            service.AddScoped<LoginService>();
            service.AddScoped<DeveloperService>();
            service.AddScoped<UserManagementService>();

            //processors
            service.AddSingleton<TokenProcessor>();
            service.AddSingleton<StringEncryptor>();
            

            return service;
        }

        public static IServiceCollection Add_MongoDb_Context(this IServiceCollection service, 
                                                             IConfiguration configuration)
        {
            var connString = configuration["mongo"]!;
            service.AddMongoContext(new CampusSystemMongoContext(connString));

            //add datetime serializer
            BsonSerializer.RegisterSerializer(typeof(DateTime), new TaiwanDateTimeSerializer());

            return service;
        }

        public static IServiceCollection AddSwaggerOptions(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddSwaggerGen(option =>
            {
                // 指定 XML 註解文件的路徑
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                option.IncludeXmlComments(xmlPath);

                //在swagger上新增Authorization按鈕
                option.AddSecurityDefinition("Campus-System", new OpenApiSecurityScheme
                {
                    Description = "請輸入Token",
                    Name = "X-Campus-System-Token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "CampusSystemToken"
                });

                //
                option.OperationFilter<AuthorizeCheckOperationFilter>();

                //多頁面
                option.SwaggerDoc("General", new OpenApiInfo
                {
                    Title = "general",
                    Version = "v1",
                    Description = "general api"
                });
                option.SwaggerDoc("Developer", new OpenApiInfo
                {
                    Title = "developer",
                    Version = "v1",
                    Description = "開發者專用"
                });
            });

            return service; 
        }
    }
}
