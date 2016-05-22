/* IBrowseForFiles.cs
 *
 * Copyright (C) 2016 Joel Rein
 * All rights reserved.
 *
 * This software may be modified and distributed under the terms
 * of the BSD license.  See the LICENSE file for details.
 */
using System.ComponentModel;
using System.IO;
using Microsoft.Win32;

namespace ProcessFiles
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
