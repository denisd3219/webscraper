using HtmlAgilityPack;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webscraper.Core.PageLoader
{
	class ScrapyLoader : IPageLoader
	{
		public readonly ScrapingBrowser Browser = new ScrapingBrowser();

		public HtmlDocument Load(string URL)
		{
			WebPage webpage = null;
			try
			{
				webpage = Browser.NavigateToPage(new Uri(URL));
			}
			catch (Exception e)
			{
				Console.WriteLine("PageLoader exception: " + e.Message);
			}
			
			return webpage?.Html.OwnerDocument;
		}
	}
}
