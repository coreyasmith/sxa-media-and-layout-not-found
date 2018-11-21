using System;
using System.Net;
using System.Web;
using CoreySmith.Feature.ErrorHandling.Services;
using Sitecore.Abstractions;
using Sitecore.Data.Items;
using Sitecore.Web;
using Sitecore.XA.Foundation.MediaRequestHandler.Pipelines.MediaRequestHandler;

namespace CoreySmith.Feature.ErrorHandling.Pipelines.MediaRequestHandler
{
  public class HandleNotFound : ProcessMediaRequestProcessor
  {
    private readonly IErrorItemResolver _errorItemResolver;
    private readonly BaseLinkManager _linkManager;
    private readonly Func<HttpServerUtilityBase> _httpServerThunk;

    public HandleNotFound(IErrorItemResolver errorItemResolver, BaseLinkManager linkManager, Func<HttpServerUtilityBase> httpServerThunk)
    {
      _errorItemResolver = errorItemResolver ?? throw new ArgumentNullException(nameof(errorItemResolver));
      _linkManager = linkManager ?? throw new ArgumentNullException(nameof(linkManager));
      _httpServerThunk = httpServerThunk ?? throw new ArgumentNullException(nameof(httpServerThunk));
    }

    public override void Process(MediaRequestHandlerArgs handlerArgs)
    {
      if (handlerArgs.Media != null) return;

      var notFoundUrl = GetNotFoundUrl();
      if (string.IsNullOrWhiteSpace(notFoundUrl)) return;

      var httpServer = _httpServerThunk();
      httpServer.TransferRequest(notFoundUrl);
      handlerArgs.Result = true;
      handlerArgs.AbortPipeline();
    }

    private string GetNotFoundUrl()
    {
      var notFoundItem = _errorItemResolver.GetNotFoundItem();
      if (notFoundItem == null) return null;

      var baseUrl = GetItemUrl(notFoundItem);
      var queryString = GetStatusCodeQueryString();
      return WebUtil.AddQueryString(baseUrl, queryString);
    }

    private string GetItemUrl(Item item)
    {
      var url = _linkManager.GetItemUrl(item);
      return url;
    }

    private static string[] GetStatusCodeQueryString()
    {
      return new[]
      {
        HttpUtility.UrlEncode(Constants.MediaRequestStatusCodeKey),
        HttpStatusCode.NotFound.ToString()
      };
    }
  }
}
