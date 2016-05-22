/* ProcessFilesApp.cs
 *
 * Copyright (C) 2016 Joel Rein
 * All rights reserved.
 *
 * This software may be modified and distributed under the terms
 * of the BSD license.  See the LICENSE file for details.
 */
using System;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace ProcessFiles
{
	public class ProcessFilesApp : ApplicationContext
	{
		public static IApp Create(Action work, Action<Exception> onFailure)
		{
			var context = new ProcessFilesApp();
			var app = new IoWindowViewModel(context, work, onFailure);
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
