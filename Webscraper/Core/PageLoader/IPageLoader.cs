using HtmlAgilityPack;

namespace Webscraper.Core.PageLoader
{
	public interface IPageLoader
	{
		HtmlDocument Load(string URL);
	}
}
