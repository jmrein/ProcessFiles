/* IoWindowViewModel.cs
 *
 * Copyright (C) 2016 Joel Rein
 * All rights reserved.
 *
 * This software may be modified and distributed under the terms
 * of the BSD license.  See the LICENSE file for details.
 */
using System;
using System.ComponentModel;
using System.IO;
using System.Reactive.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Win32;
using ReactiveUI;

namespace ProcessFiles
{
	internal class IoWindowViewModel : ReactiveObject, IApp
	{
		private readonly BackgroundWorker worker = new BackgroundWorker {WorkerReportsProgress = true};
		private readonly ObservableAsPropertyHelper<bool> isInProgressHelper;
		private readonly ProcessFilesApp context;
		private CancellationTokenSource tokenSource;
		private CancellationToken cancellationToken;
		private float progress;
		private string title;

		public ReactiveCommand<object> StartCommand { get; }
		public ReactiveCommand<object> CancelCommand { get; }
		public IBrowseForFiles OpenFile { get; }
		public IBrowseForFiles SaveFile { get; }
		internal Window Owner { get; set; }
		public bool IsInProgress => isInProgressHelper.Value;

		internal CancellationTokenSource TokenSource
		{
			get { return tokenSource; }
			set { this.RaiseAndSetIfChanged(ref tokenSource, value); }
		}

		public CancellationToken CancellationToken
		{
			get { return cancellationToken; }
			private set { this.RaiseAndSetIfChanged(ref cancellationToken, value); }
		}

		public float Progress
		{
			get { return progress; }
			set { this.RaiseAndSetIfChanged(ref progress, value); }
		}

		public string Title
		{
			get { return title; }
			set { this.RaiseAndSetIfChanged(ref title, value); }
		}

		internal IoWindowViewModel(ProcessFilesApp context, Action work, Action<Exception> onFailure)
		{
			this.context = context;
			var isInProgress = this.WhenAnyValue(me => me.TokenSource).Select(source => source != null).ObserveOn(Dispatcher.CurrentDispatcher);
			isInProgressHelper = this.WhenAnyValue(me => me.TokenSource).Select(source => source != null).ToProperty(this, me => me.IsInProgress, true);
			OpenFile = new InputFileBrowseViewModel(isInProgress.Select(f => !f));
			SaveFile = new OutputFileBrowseViewModel(isInProgress.Select(f => !f), OpenFile);
			worker.DoWork += (o, e) =>
			{
				work();
				e.Result = 0;
			};
			worker.RunWorkerCompleted += (o, e) =>
			{
				if (TokenSource.IsCancellationRequested)
				{
					Progress = 0;
				}
				else if (e.Error != null)
				{
					MessageBox.Show(Owner, e.Error.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
					Progress = 0;
				}
				else
				{
					new ExportCompletedWindow(new ExportCompletedViewModel(Title, SaveFile.File)) {Owner = Owner}.ShowDialog();
				}
				TokenSource = null;
				if (e.Error != null)
				{
					onFailure(e.Error);
				}
			};
			worker.ProgressChanged += (o, e) => Progress = e.ProgressPercentage;
			StartCommand = ReactiveCommand.Create(this.WhenAnyValue(me => me.OpenFile.File, me => me.SaveFile.File, me => me.TokenSource)
				.Select(CanStart).ObserveOn(Dispatcher.CurrentDispatcher));
			CancelCommand = ReactiveCommand.Create(isInProgress);
			StartCommand.Subscribe(_ =>
			{
				TokenSource = new CancellationTokenSource();
				worker.RunWorkerAsync();
			});
			CancelCommand.Subscribe(_ => Cancel());
			isInProgress.Where(f => f).Subscribe(source => CancellationToken = TokenSource.Token);
			this.WhenAnyValue(me => me.Title).Where(title => !string.IsNullOrEmpty(title)).Subscribe(title => Owner.Title = title);
		}

		private bool CanStart(Tuple<FileInfo, FileInfo, CancellationTokenSource> args)
		{
			return OpenFile.File != null && SaveFile.File != null && TokenSource == null;
		}

		public void Start()
		{
			context.Start();
		}

		public void Cancel()
		{
			tokenSource.Cancel();
		}
	}
}
