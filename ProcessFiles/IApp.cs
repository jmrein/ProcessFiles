/* IApp.cs
 *
 * Copyright (C) 2016 Joel Rein
 * All rights reserved.
 *
 * This software may be modified and distributed under the terms
 * of the BSD license.  See the LICENSE file for details.
 */
using System.ComponentModel;
using System.Threading;

namespace ProcessFiles
{
	public interface IApp : INotifyPropertyChanged
	{
		IBrowseForFiles OpenFile { get; }
		IBrowseForFiles SaveFile { get; }
		float Progress { get; set; }
		string Title { get; set; }
		CancellationToken CancellationToken { get; }
		void Start();
		void Cancel();
		bool IsInProgress { get; }
	}
}
