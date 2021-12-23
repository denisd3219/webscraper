using System;
using HtmlAgilityPack;

namespace Webscraper.Core.PageLoader
{
	class HAPLoader : IPageLoader
	{
		public HtmlDocument Load(string URL)
		{
			HtmlWeb web = new HtmlWeb();
			return web.Load(URL);
		}
	}
}
