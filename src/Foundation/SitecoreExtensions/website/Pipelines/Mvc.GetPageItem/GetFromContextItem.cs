using System;
using Sitecore.Mvc.Pipelines.Response.GetPageItem;
using Sitecore.XA.Foundation.Abstractions;

namespace CoreySmith.Foundation.SitecoreExtensions.Pipelines.Mvc.GetPageItem
{
  public class GetFromContextItem : GetPageItemProcessor
  {
    private readonly IContext _context;

    public GetFromContextItem(IContext context)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public override void Process(GetPageItemArgs args)
    {
      if (AbortProcessor(args)) return;
      args.Result = _context.Item;
    }

    private bool AbortProcessor(GetPageItemArgs args)
    {
      if (!_context.Items.Contains(Constants.CustomContextItemKey)) return true;
      if (_context.Item == null) return true;
      if (args.Result != null) return true;
      return false;
    }
  }
}
