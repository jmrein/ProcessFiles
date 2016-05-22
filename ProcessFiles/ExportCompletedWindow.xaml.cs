/* ExportCompletedWindow.cs
 *
 * Copyright (C) 2016 Joel Rein
 * All rights reserved.
 *
 * This software may be modified and distributed under the terms
 * of the BSD license.  See the LICENSE file for details.
 */
using System;
using System.Reactive.Linq;
using System.Windows;
using ReactiveUI;

namespace ProcessFiles
{
	public partial class ExportCompletedWindow : Window
	{
		private readonly IDisposable subscription;

		public ExportCompletedWindow(ExportCompletedViewModel viewModel)
		{
			InitializeComponent();
			DataContext = viewModel;
			viewModel.WhenAnyValue(me => me.Title).Where(title => title != null).Subscribe(title => Title = title);
			subscription = viewModel.Result.Subscribe(result =>
			{
				DialogResult = result;
				subscription.Dispose();
			});
		}
	}
}
