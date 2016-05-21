using System;
using System.Reactive.Linq;
using System.Windows;
using ReactiveUI;

namespace FileIO
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
