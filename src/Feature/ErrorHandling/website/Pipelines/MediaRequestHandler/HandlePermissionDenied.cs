using System;
using System.Web;
using Sitecore.Resources.Media;
using Sitecore.SecurityModel;
using Sitecore.XA.Foundation.Abstractions;
using Sitecore.XA.Foundation.MediaRequestHandler.Pipelines.MediaRequestHandler;

namespace CoreySmith.Feature.ErrorHandling.Pipelines.MediaRequestHandler
{
  public class HandlePermissionDenied : ProcessMediaRequestProcessor
  {
    private readonly IContext _context;
    private readonly Func<HttpResponseBase> _httpResponseThunk;

    public HandlePermissionDenied(IContext context, Func<HttpResponseBase> httpResponseThunk)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
      _httpResponseThunk = httpResponseThunk ?? throw new ArgumentNullException(nameof(httpResponseThunk));
    }

    public override void Process(MediaRequestHandlerArgs handlerArgs)
    {
      if (handlerArgs.Media != null) return;
      if (!PermissionDenied(handlerArgs)) return;

      var loginUrl = GetLoginUrl();
      if (string.IsNullOrWhiteSpace(loginUrl)) return;

      var httpResponse = _httpResponseThunk();
      httpResponse.Redirect(loginUrl);
      handlerArgs.Result = true;
      handlerArgs.AbortPipeline();
    }

    private static bool PermissionDenied(MediaRequestHandlerArgs handlerArgs)
    {
      using (new SecurityDisabler())
      {
        handlerArgs.Media = MediaManager.GetMedia(handlerArgs.Request.MediaUri);
      }
      return handlerArgs.Media != null;
    }

    private string GetLoginUrl()
    {
      return _context.Site?.LoginPage;
    }
  }
}
