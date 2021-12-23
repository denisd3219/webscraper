using System.Collections.Generic;
using Webscraper.Core.DataTypes;
using Webscraper.Core.JobOutput;
using Webscraper.Core.PageLoader;
using Webscraper.Core.ScrapingFilter;

namespace Webscraper.Core.ScrapingJob
{
	class ItemsScrapingJob : IScrapingJob
	{
		public string Name { get; set; }
		public string URL { get; set; }
		public IPageLoader PageLoader { get; set; }
		public IScrapingFilter<List<ItemProps>> ItemFilter { get; set; }
		public IJobOutput<ItemProps> Output { get; set; }

		public void Process()
		{
			Output.Write(ItemFilter.Execute(PageLoader.Load(URL)));
		}
	}
}
