using System;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Sites;
using Sitecore.XA.Foundation.Abstractions;
using Sitecore.XA.Foundation.Multisite;

namespace CoreySmith.Foundation.Abstractions.Sites
{
  public class ContextSite : IContextSite
  {
    private readonly IContext _context;
    private readonly IMultisiteContext _multisiteContext;

    private Database ContentDatabase => _context.ContentDatabase ?? _context.Database;

    public Item SiteItem => GetSiteItem(_context.Site);
    public Item SettingsItem => GetSettingsItem(_context.Site);

    public ContextSite(IContext context, IMultisiteContext multisiteContext)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
      _multisiteContext = multisiteContext ?? throw new ArgumentNullException(nameof(multisiteContext));
    }

    private Item GetSiteItem(SiteContext site)
    {
      return site == null ? null : ContentDatabase.GetItem(site.RootPath);
    }

    private Item GetSettingsItem(SiteContext site)
    {
      var siteItem = GetSiteItem(site);
      return siteItem == null ? null : _multisiteContext.GetSettingsItem(siteItem);
    }
  }
}
