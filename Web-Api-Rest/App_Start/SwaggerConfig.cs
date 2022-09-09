using System.Web.Http;
using WebActivatorEx;
using Web_Api_Rest;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Web_Api_Rest
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "API Politecnico");
                    c.ApiKey("apiKey")
                        .Description("API Key Authentication")
                        .Name("X-ApiKey")
                        .In("header");
                })
                
                .EnableSwaggerUi(c =>
                {
                    c.EnableApiKeySupport("X-ApiKey", "header");
                });
        }
    }
}
