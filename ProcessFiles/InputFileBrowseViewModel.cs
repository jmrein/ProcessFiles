using System;
using System.IO;
using Microsoft.Win32;

namespace ProcessFiles
{
	public class InputFileBrowseViewModel : FileBrowseViewModel
	{
		public InputFileBrowseViewModel(IObservable<bool> isEnabled) : base(isEnabled)
		{
			Dialog = new OpenFileDialog();
		}

		protected override FileInfo Validate(string path)
		{
			var file = base.Validate(path);
			if (file != null && !file.Exists)
			{
				throw new FileNotFoundException();
			}
			return file;
		}
	}
}
