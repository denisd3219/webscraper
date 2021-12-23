using System;
using System.Collections.Generic;
using Webscraper.Core.DataTypes;

namespace Webscraper.Core.JobOutput
{
	class ItemConsoleOutput : IJobOutput<ItemProps>
	{
		public void Write(ItemProps output)
		{
			Console.WriteLine("----------------------");
			foreach (var item in output)
				Console.WriteLine(item.Key + ": " + item.Value);
		}

		public void Write(IEnumerable<ItemProps> output)
		{
			foreach (ItemProps item in output)
				Write(item);
		}
	}
}
