using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Training.DotNetCore.Project.API
{
    public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider apiVersionDescriptionProvider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            this.apiVersionDescriptionProvider = apiVersionDescriptionProvider;
        }
        public void Configure(string? name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var item in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                if (options.SwaggerGeneratorOptions.SwaggerDocs.ContainsKey(item.GroupName))
                {
                    options.SwaggerGeneratorOptions.SwaggerDocs.Remove(item.GroupName);
                }
                options.SwaggerDoc(item.GroupName, CreateVersionInfo(item));

            }
        }

        private OpenApiInfo CreateVersionInfo(ApiVersionDescription description) 
        {
            var info = new OpenApiInfo
            {
                Title = "Your Version API",
                Description = description.ApiVersion.ToString()
            };
            return info;
        }
    }
}
