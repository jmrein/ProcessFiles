using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using FileIO;
using Microsoft.VisualBasic.FileIO;

namespace Demo
{
	internal static class Program
	{
		private static IApp app;

		[STAThread]
		private static void Main()
		{
			app = FileIoApp.Create(CsvToTab);
			app.Title = "Demo app";
			app.OpenFile.LabelText = "Input CSV file: ";
			app.OpenFile.Dialog.Filter = "CSV files|*.csv";
			app.SaveFile.LabelText = "Output TSV to: ";
			app.SaveFile.Dialog.Filter = "TSV file|*.tsv";
			app.Start();
		}

		private static void CsvToTab()
		{
			using (var stream = app.OpenFile.File.OpenRead())
			using (var reader = new TextFieldParser(stream) { Delimiters = new[] { CultureInfo.CurrentCulture.TextInfo.ListSeparator } })
			using (var writer = new StreamWriter(app.SaveFile.File.OpenWrite()))
			{
				var fields = reader.ReadFields();
				while (fields != null)
				{
					app.CancellationToken.ThrowIfCancellationRequested();
					writer.WriteLine(string.Join("\t", fields.Select(f => f.Replace("\t", "\\t"))));
					Thread.Sleep(3);
					app.Progress = (float)stream.Position / stream.Length;
					fields = reader.ReadFields();
				}
			}
		}
	}
}
