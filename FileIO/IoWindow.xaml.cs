using System.Windows;

namespace FileIO
{
	public partial class IoWindow : Window
	{
		public IoWindow(IApp app)
		{
			InitializeComponent();
			DataContext = app;

			Closing += (o, e) =>
			{
				if (app.IsInProgress)
				{
					app.Cancel();
					e.Cancel = true;
				}
			};
		}
	}
}
