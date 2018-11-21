using System;
using System.Net;
using Sitecore.Pipelines.HttpRequest;

namespace CoreySmith.Feature.ErrorHandling.Pipelines.HttpRequestProcessed
{
  public class SetMediaRequestStatusCode : HttpRequestProcessor
  {
    public override void Process(HttpRequestArgs args)
    {
      var httpStatus = args.HttpContext.Request.QueryString[Constants.MediaRequestStatusCodeKey];
      if (string.IsNullOrEmpty(httpStatus)) return;

      if (!Enum.TryParse<HttpStatusCode>(httpStatus, out var httpStatusCode)) return;
      if (httpStatusCode != HttpStatusCode.NotFound) return;

      args.HttpContext.Response.StatusCode = (int)httpStatusCode;
    }
  }
}
