using System.ComponentModel;
using System.IO;
using Microsoft.Win32;

namespace FileIO
{
	public interface IBrowseForFiles : INotifyPropertyChanged
	{
		string LabelText { get; set; }
		string BrowseText { get; set; }
		string Location { get; set; }
		FileInfo File { get; }
		FileDialog Dialog { get; set; }
	}
}
