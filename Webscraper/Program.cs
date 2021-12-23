using System;
using System.Collections.Generic;
using Webscraper.Core.PageLoader;
using Webscraper.Core.ScrapingFilter;
using Webscraper.Core.ScrapingJob;
using Webscraper.Core.JobOutput;
using Webscraper.Core.JobQueue;

namespace Webscraper
{
	class Program
	{
		static void Main(string[] args)
		{
			var habraJob = new ItemsScrapingJob
			{
				URL = "https://habr.com/ru/all/",
				PageLoader = new HAPLoader(),
				ItemFilter = new ItemsXPathFilter(
					"//article[contains(@class, 'tm-articles-list__item')]",
					new Dictionary<string, string>()
					{
						{ "Name", ".//a[contains(@class, 'tm-article-snippet__title-link')]" },
						{ "Hubs", ".//div[contains(@class, 'tm-article-snippet__hubs')]" }
					}
				),
				Output = new ItemConsoleOutput()
			};

			habraJob.Process();
			
			var concOutput = new ItemConcurrentQueueOutput();
			Console.WriteLine("Parsing started");
			int todo = 0;
			for (int i = 0; i < 4; i++)
			{
				var jobQueue = new JobQueue();
				jobQueue.OnEmpty += new JobQueue.Empty(() => { Console.WriteLine(string.Format("Queue empty")); todo++; });
				
				for (int j = 1; j <= 25; j++)
				{
					jobQueue.Enqueue(
						new ItemsScrapingJob
						{
							URL = string.Format("https://naberezhnye-chelny.leroymerlin.ru/search/?q=труба&page={0}", i*j),
							PageLoader = new ScrapyLoader(),
							ItemFilter = new ItemsXPathFilter(
								"//div[contains(@class, 'largeCard')][@data-qa-product]",
								new Dictionary<string, string>()
								{
								{ "Article", ".//span[@data-qa-product-article]" },
								{ "Name", ".//a[contains(@data-qa, 'product-name')]" },
								{ "Price", ".//div[contains(@data-qa, 'product-primary-price')]" },
								{ "Reviews", ".//div[@data-qa-product-reviews]" }
								}
							),
							Output = concOutput
						}
					);
				}
			}

			while (todo < 4) { }
			Console.WriteLine("Parsing done, writing");
			var xlOut = new ItemExcelFileOutput();
			xlOut.Write(concOutput.Output);
			Console.WriteLine("Done");
			

			Console.ReadKey();
		}
	}
}
