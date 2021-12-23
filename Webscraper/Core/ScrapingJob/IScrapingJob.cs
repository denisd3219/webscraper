using HtmlAgilityPack;
using System.Collections.Generic;
using Webscraper.Core.JobOutput;

namespace Webscraper.Core.ScrapingJob
{
	public interface IScrapingJob
	{
		void Process();
	}
}