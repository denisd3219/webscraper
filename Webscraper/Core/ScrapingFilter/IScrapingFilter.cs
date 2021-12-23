using HtmlAgilityPack;

namespace Webscraper.Core.ScrapingFilter
{
	public interface IScrapingFilter<T>
	{
		T Execute(HtmlDocument input);
	}
}
