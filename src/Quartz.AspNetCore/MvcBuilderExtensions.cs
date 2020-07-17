using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Quartz
{
    public class ControllerEndpointRouteBuilderExtensions
    {
        public static IEndpointConventionBuilder MapHealthChecksUI(
            this IEndpointRouteBuilder endpoints,
            Action<Options> setupOptions = null)
        {
            Options options = new Options();
            if (setupOptions != null)
                setupOptions(options);
            EndpointRouteBuilderExtensions.EnsureValidApiOptions(options);
            RequestDelegate requestDelegate1 = builder.CreateApplicationBuilder().UseMiddleware<UIApiEndpointMiddleware>().Build();
            RequestDelegate requestDelegate2 = builder.CreateApplicationBuilder().UseMiddleware<UIWebHooksApiMiddleware>().Build();
            IEnumerable<IEndpointConventionBuilder> second = new UIEndpointsResourceMapper((IUIResourcesReader) new UIEmbeddedResourcesReader(typeof (UIResource).Assembly)).Map(builder, options);
            return (IEndpointConventionBuilder) new HealthCheckUIConventionBuilder((IEnumerable<IEndpointConventionBuilder>) new List<IEndpointConventionBuilder>(((IEnumerable<IEndpointConventionBuilder>) new IEndpointConventionBuilder[2]
            {
                builder.Map(options.ApiPath, requestDelegate1).WithDisplayName<IEndpointConventionBuilder>("HealthChecks UI Api"),
                builder.Map(options.WebhookPath, requestDelegate2).WithDisplayName<IEndpointConventionBuilder>("HealthChecks UI Webhooks")
            }).Union<IEndpointConventionBuilder>(second)));
        }

        private static void EnsureValidApiOptions(Options options)
        {
            Action<string, string> action = (Action<string, string>) ((path, argument) =>
            {
                if (string.IsNullOrEmpty(path) || !path.StartsWith("/"))
                    throw new ArgumentException("The value for customized path can't be null and need to start with / character.", argument);
            });
            action(options.ApiPath, "ApiPath");
            action(options.UIPath, "UIPath");
            action(options.WebhookPath, "WebhookPath");
        }
    }
}