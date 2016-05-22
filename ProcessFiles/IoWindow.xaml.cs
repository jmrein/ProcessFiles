/* IoWindow.cs
 *
 * Copyright (C) 2016 Joel Rein
 * All rights reserved.
 *
 * This software may be modified and distributed under the terms
 * of the BSD license.  See the LICENSE file for details.
 */
using System.Windows;

namespace ProcessFiles
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
