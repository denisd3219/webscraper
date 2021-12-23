using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Webscraper.Core.DataTypes;

namespace Webscraper.Core.JobOutput
{
	class ItemConcurrentQueueOutput : IJobOutput<ItemProps>
	{
		public ConcurrentQueue<ItemProps> Output { get; set; } = new ConcurrentQueue<ItemProps>();

		public void Write(ItemProps output)
		{
			Output.Enqueue(output);
		}

		public void Write(IEnumerable<ItemProps> output)
		{
			foreach (ItemProps item in output)
				Output.Enqueue(item);
		}
	}
}
