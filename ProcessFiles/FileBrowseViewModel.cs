/* FileBrowseViewModel.cs
 *
 * Copyright (C) 2016 Joel Rein
 * All rights reserved.
 *
 * This software may be modified and distributed under the terms
 * of the BSD license.  See the LICENSE file for details.
 */
using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using ReactiveUI;

namespace ProcessFiles
{
	public class FileBrowseViewModel : ReactiveObject, IBrowseForFiles
	{
		private readonly ObservableAsPropertyHelper<bool> isEnabledHelper;
		public ReactiveCommand<object> BrowseCommand { get; }
		private string labelText = "Input file: ";
		private string browseText = "...";
		private string location;
		private FileInfo file;

		public bool IsEnabled => isEnabledHelper.Value;

		internal FileBrowseViewModel(IObservable<bool> isEnabled)
		{
			isEnabledHelper = isEnabled.ToProperty(this, me => me.IsEnabled, true);
			BrowseCommand = ReactiveCommand.Create(isEnabled);
			BrowseCommand.Subscribe(_ => 
			{
				if (Dialog.ShowDialog(Owner) == true)
				{
					Location = Dialog.FileName;
				}
			});
			this.WhenAnyValue(me => me.Location).Subscribe(path => File = (path != null) ? new FileInfo(path) : null);
		}

		public string LabelText
		{
			get { return labelText; }
			set { this.RaiseAndSetIfChanged(ref labelText, value); }
		}

		public string BrowseText
		{
			get { return browseText; }
			set { this.RaiseAndSetIfChanged(ref browseText, value); }
		}

		public string Location
		{
			get { return location; }
			set { this.RaiseAndSetIfChanged(ref location, value); }
		}

		public FileInfo File
		{
			get { return file; }
			private set { this.RaiseAndSetIfChanged(ref file, value); }
		}

		public FileDialog Dialog { get; set; }
		public Window Owner { get; set; }
	}
}
