using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using Webscraper.Core.DataTypes;

namespace Webscraper.Core.ScrapingFilter
{
	class ItemsXPathFilter : IScrapingFilter<List<ItemProps>>
	{
		private string itemXPath;
		private Dictionary<string, string> propsXPaths;

		public ItemsXPathFilter(string itemXPath, Dictionary<string, string> propsXPaths)
		{
			this.itemXPath = itemXPath;
			this.propsXPaths = propsXPaths;
		}

		public List<ItemProps> Execute(HtmlDocument input)
		{
			List<ItemProps> res = new List<ItemProps>();
			try
			{
				foreach (HtmlNode itemNode in input.DocumentNode.SelectNodes(itemXPath))
				{
					ItemProps props = new ItemProps();
					foreach (KeyValuePair<string, string> propPath in propsXPaths)
						props.Add(propPath.Key, itemNode.SelectSingleNode(propPath.Value)?.InnerText);
					res.Add(props);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("ScrapingFilter exception: " + e.Message);
			}
			return res;
		}
	}
}
