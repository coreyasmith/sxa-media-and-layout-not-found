using System;
using CoreySmith.Foundation.Abstractions.Sites;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using SxaErrorHandlingTemplates = Sitecore.XA.Feature.ErrorHandling.Templates;

namespace CoreySmith.Feature.ErrorHandling.Services
{
  public class ErrorItemResolver : IErrorItemResolver
  {
    private readonly IContextSite _contextSite;

    public ErrorItemResolver(IContextSite contextSite)
    {
      _contextSite = contextSite ?? throw new ArgumentNullException(nameof(contextSite));
    }

    public Item GetNotFoundItem()
    {
      var notFoundItem = GetErrorItem(SxaErrorHandlingTemplates._ErrorHandling.Fields.Error404Page.ToString());
      return notFoundItem;
    }

    private Item GetErrorItem(string fieldId)
    {
      var settingsItem = GetContextSiteSettingsItem();
      ReferenceField errorItemField = settingsItem?.Fields[fieldId];
      return errorItemField?.TargetItem;
    }

    private Item GetContextSiteSettingsItem()
    {
      return _contextSite.SettingsItem;
    }
  }
}
