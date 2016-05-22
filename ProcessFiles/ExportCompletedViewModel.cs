/* ExportCompletedViewModel.cs
 *
 * Copyright (C) 2016 Joel Rein
 * All rights reserved.
 *
 * This software may be modified and distributed under the terms
 * of the BSD license.  See the LICENSE file for details.
 */

using System;
using System.Diagnostics;
using System.IO;
using System.Reactive.Linq;
using ReactiveUI;

namespace ProcessFiles
{
	public class ExportCompletedViewModel : ReactiveObject
	{
		private FileInfo file;
		private string title;
		public ReactiveCommand<object> OpenFileCommand { get; }
		public ReactiveCommand<object> OpenFolderCommand { get; }
		public ReactiveCommand<object> CloseCommand { get; } = ReactiveCommand.Create();
		public IObservable<bool?> Result { get; }

		public FileInfo File
		{
			get { return file; }
			set { this.RaiseAndSetIfChanged(ref file, value); }
		}

		public string Title
		{
			get { return title; }
			set { this.RaiseAndSetIfChanged(ref title, value); }
		}

		public ExportCompletedViewModel(string title, FileInfo file)
		{
			Title = title;
			File = file;
			OpenFileCommand = ReactiveCommand.Create(this.WhenAnyValue(me => me.File).Select(f => f?.Exists == true));
			OpenFolderCommand = ReactiveCommand.Create(this.WhenAnyValue(me => me.File).Select(f => f?.Directory?.Exists == true));
			OpenFileCommand.Subscribe(_ => Process.Start(File.FullName));
			OpenFolderCommand.Subscribe(_ => Process.Start(File.DirectoryName));
			Result = CloseCommand.Select(_ => (bool?) true);
		}
	}
}
