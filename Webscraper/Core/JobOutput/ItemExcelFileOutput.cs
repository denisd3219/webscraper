using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Webscraper.Core.DataTypes;
using Excel = Microsoft.Office.Interop.Excel;

namespace Webscraper.Core.JobOutput
{
	class ItemExcelFileOutput : IJobOutput<ItemProps>
	{
		public string OutputFileDir { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		public string OutputFileName { get; set; } = "ItemExcelFileOutput";
		public bool Append { get; set; } = true;

		public void Write(ItemProps output)
		{
			Write(new ItemProps[] {output});
		}

		public void Write(IEnumerable<ItemProps> output)
		{
			string filePath = OutputFileDir + "\\" + OutputFileName + ".xls";

			Excel.Application app = new Excel.Application();

			if (app == null)
				return;

			app.DisplayAlerts = false;

			Excel.Workbook wb;
			if (Append && File.Exists(filePath))
				wb = app.Workbooks.Open(filePath);
			else
				wb = app.Workbooks.Add();

			try
			{
				Excel.Worksheet sheet = (Excel.Worksheet)wb.Sheets.Add();
				sheet.Name = "Scraping " + DateTime.Now.ToString("HH-mm-ss dd_mm_yyyy");

				List<string> keyList = new List<string>();
				foreach (var props in output)
					keyList.AddRange(props.Keys.ToList().Where(key => !keyList.Contains(key)));

				for (int i = 1; i <= keyList.Count; i++)
					sheet.Cells[1, i] = keyList[i - 1];

				int row = 2;
				foreach (var props in output)
				{
					foreach (var prop in props)
						sheet.Cells[row, keyList.IndexOf(prop.Key) + 1] = prop.Value;
					row++;
				}

				app.ActiveWorkbook.SaveAs(filePath, Excel.XlFileFormat.xlWorkbookNormal);
			}
			catch (Exception)
			{
				throw;
			}
			finally
			{
				wb.Close();
				app.Quit();
			}
		}
	}
}
