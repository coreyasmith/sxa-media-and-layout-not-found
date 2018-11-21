using System.Web.Routing;

namespace CoreySmith.Foundation.Abstractions.Services
{
  public interface IRouteDataProvider
  {
    RouteData GetRouteData();
  }
}
