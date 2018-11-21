using System;
using System.Web;
using System.Web.Routing;

namespace CoreySmith.Foundation.Abstractions.Services
{
  public class RouteDataProvider : IRouteDataProvider
  {
    private readonly Func<HttpContextBase> _httpContextThunk;

    public RouteDataProvider(Func<HttpContextBase> httpContextThunk)
    {
      _httpContextThunk = httpContextThunk ?? throw new ArgumentNullException(nameof(httpContextThunk));
    }

    public RouteData GetRouteData()
    {
      var httpContext = _httpContextThunk();
      return RouteTable.Routes.GetRouteData(httpContext);
    }
  }
}
