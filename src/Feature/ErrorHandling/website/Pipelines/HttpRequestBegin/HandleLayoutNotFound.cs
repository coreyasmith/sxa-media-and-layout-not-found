using System;
using System.Net;
using CoreySmith.Feature.ErrorHandling.Services;
using CoreySmith.Foundation.Abstractions.Services;
using Sitecore.Data.Items;
using Sitecore.Pipelines.HttpRequest;
using Sitecore.XA.Foundation.Abstractions;
using SitecoreExtensionsConstants = CoreySmith.Foundation.SitecoreExtensions.Constants;

namespace CoreySmith.Feature.ErrorHandling.Pipelines.HttpRequestBegin
{
  public class HandleLayoutNotFound : HttpRequestProcessor
  {
    private readonly IContext _context;
    private readonly IErrorItemResolver _errorItemResolver;
    private readonly IRouteDataProvider _routeDataProvider;

    public HandleLayoutNotFound(IContext context, IErrorItemResolver errorItemResolver, IRouteDataProvider routeDataProvider)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
      _errorItemResolver = errorItemResolver ?? throw new ArgumentNullException(nameof(errorItemResolver));
      _routeDataProvider = routeDataProvider ?? throw new ArgumentNullException(nameof(routeDataProvider));
    }

    public override void Process(HttpRequestArgs args)
    {
      if (AbortProcessor(args)) return;

      var notFoundItem = _errorItemResolver.GetNotFoundItem();
      if (notFoundItem == null) return;

      var layoutFilePath = notFoundItem.Visualization?.Layout?.FilePath;
      if (string.IsNullOrEmpty(layoutFilePath)) return;

      _context.Item = notFoundItem;
      _context.Page.FilePath = layoutFilePath;
      _context.Items["httpStatus"] = (int)HttpStatusCode.NotFound;
      _context.Items[SitecoreExtensionsConstants.CustomContextItemKey] = true;
      args.HttpContext.Response.TrySkipIisCustomErrors = true;
    }

    private bool AbortProcessor(HttpRequestArgs args)
    {
      if (_context.Site == null) return true;
      if (_context.Database == null) return true;
      if (!string.IsNullOrEmpty(_context.Page.FilePath)) return true;
      if (_routeDataProvider.GetRouteData() != null) return true;
      if (args.PermissionDenied) return true;
      return false;
    }
  }
}
