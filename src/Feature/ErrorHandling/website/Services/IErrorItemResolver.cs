using Sitecore.Data.Items;

namespace CoreySmith.Feature.ErrorHandling.Services
{
  public interface IErrorItemResolver
  {
    Item GetNotFoundItem();
  }
}
