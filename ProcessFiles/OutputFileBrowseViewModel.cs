using System;
using System.IO;
using Microsoft.Win32;

namespace ProcessFiles
{
	public class OutputFileBrowseViewModel : FileBrowseViewModel
	{
		private readonly IBrowseForFiles input;

		public OutputFileBrowseViewModel(IObservable<bool> isEnabled, IBrowseForFiles input) : base(isEnabled)
		{
			this.input = input;
			Dialog = new SaveFileDialog {OverwritePrompt = true, CheckPathExists = true};
			LabelText = "Output file: ";
		}

		protected override FileInfo Validate(string path)
		{
			var file = base.Validate(path);
			if (file.Directory?.Exists != true)
			{
				throw new DirectoryNotFoundException("That path does not exist.");
			}
			if (string.Equals(path, input.File?.FullName, StringComparison.OrdinalIgnoreCase))
			{
				throw new ArgumentException("Cannot write to the input file.");
			}
			return file;
		}
	}
}
