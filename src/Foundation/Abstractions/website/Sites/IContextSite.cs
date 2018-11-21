using Sitecore.Data.Items;

namespace CoreySmith.Foundation.Abstractions.Sites
{
  public interface IContextSite
  {
    Item SiteItem { get; }
    Item SettingsItem { get; }
  }
}
