using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Webscraper.Core.DataTypes;

namespace Webscraper.Core.JobOutput
{
	class ItemJSONFileOutput : IJobOutput<ItemProps>
	{
		public string OutputFileDir { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		public string OutputFileName { get; set; } = "ItemJSONFileOutput";
		public bool Append { get; set; } = true;
		public Formatting OutputFormatting { get; set; } = Formatting.Indented;

		public void Write(ItemProps output)
		{
			Write(new ItemProps[] {output});
		}

		public void Write(IEnumerable<ItemProps> output)
		{
			string filePath = OutputFileDir + "\\" + OutputFileName + ".json";

			List<ItemProps> toWrite;
			if (Append && File.Exists(filePath))
				toWrite = JsonConvert.DeserializeObject<List<ItemProps>>(File.ReadAllText(filePath));
			else
				toWrite = new List<ItemProps>();

			toWrite.AddRange(output);

			File.WriteAllText(filePath, JsonConvert.SerializeObject(toWrite, OutputFormatting));
		}
	}
}
