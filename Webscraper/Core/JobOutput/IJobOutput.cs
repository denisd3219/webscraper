using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webscraper.Core.JobOutput
{
	public interface IJobOutput<T>
	{
		void Write(T output);
		void Write(IEnumerable<T> output);
	}
}
