using CoreySmith.Feature.ErrorHandling.Services;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace CoreySmith.Feature.ErrorHandling
{
  public class ErrorHandlingConfigurator : IServicesConfigurator
  {
    public void Configure(IServiceCollection serviceCollection)
    {
      serviceCollection.AddSingleton<IErrorItemResolver, ErrorItemResolver>();
    }
  }
}
