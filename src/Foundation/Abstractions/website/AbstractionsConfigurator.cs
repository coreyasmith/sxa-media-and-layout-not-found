using System;
using System.Web;
using CoreySmith.Foundation.Abstractions.Sites;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace CoreySmith.Foundation.Abstractions
{
  public class AbstractionsConfigurator : IServicesConfigurator
  {
    public void Configure(IServiceCollection serviceCollection)
    {
      serviceCollection.AddSingleton<IContextSite, ContextSite>();
      RegisterWebAbstractions(serviceCollection);
    }

    /// <summary>
    /// See my blog post on working with scoped services in Sitecore to understand why
    /// these services are registered this way.
    /// https://www.coreysmith.co/sitecore-dependency-injection-scoped-services/
    /// </summary>
    private static void RegisterWebAbstractions(IServiceCollection serviceCollection)
    {
      serviceCollection.AddScoped(CreateHttpResponse);
      serviceCollection.AddScoped(CreateHttpServer);

      serviceCollection.AddSingleton<Func<HttpResponseBase>>(_ => Get<HttpResponseBase>);
      serviceCollection.AddSingleton<Func<HttpServerUtilityBase>>(_ => Get<HttpServerUtilityBase>);
    }

    private static HttpResponseBase CreateHttpResponse(IServiceProvider serviceProvider)
    {
      return new HttpResponseWrapper(HttpContext.Current.Response);
    }

    private static HttpServerUtilityBase CreateHttpServer(IServiceProvider serviceProvider)
    {
      return new HttpServerUtilityWrapper(HttpContext.Current.Server);
    }

    private static T Get<T>()
    {
      return ServiceLocator.ServiceProvider.GetService<T>();
    }
  }
}
