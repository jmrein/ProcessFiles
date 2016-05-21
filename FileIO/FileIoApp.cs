using System;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace FileIO
{
	public class FileIoApp : ApplicationContext
	{
		public static IApp Create(Action work)
		{
			var context = new FileIoApp();
			var app = new IoAppViewModel(context, work);
			var ioWindow = new IoWindow(app);
			app.Owner = ioWindow;
			ioWindow.Closed += (o, e) => context.ExitThread();
			ElementHost.EnableModelessKeyboardInterop(ioWindow);
			ioWindow.Show();
			return app;
		}

		public void Start()
		{
			Application.Run(this);
		}
	}
}
