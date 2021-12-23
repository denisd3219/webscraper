using System;
using System.Collections.Concurrent;
using System.Threading;
using Webscraper.Core.ScrapingJob;

namespace Webscraper.Core.JobQueue
{
	public class JobQueue
	{
		private ConcurrentQueue<IScrapingJob> _jobs = new ConcurrentQueue<IScrapingJob>();
		private bool emitEmpty = false;

		public delegate void Empty();
		public Empty OnEmpty { get; set; }

		public JobQueue()
		{
			var thread = new Thread(new ThreadStart(OnStart));
			thread.IsBackground = true;
			thread.Start();
		}

		public void Enqueue(IScrapingJob job)
		{
			_jobs.Enqueue(job);
			emitEmpty = true;
		}

		private void OnStart()
		{
			while (true)
			{
				if (_jobs.TryDequeue(out IScrapingJob job))
					job.Process();
				else if (emitEmpty)
				{
					OnEmpty();
					emitEmpty = false;
				}
			}
		}
	}
}
