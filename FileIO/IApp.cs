using System.ComponentModel;
using System.Threading;

namespace FileIO
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
