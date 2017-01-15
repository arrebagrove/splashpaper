#pragma once

using namespace Platform;
using namespace Windows::Storage;

namespace OptimizedTasks
{
    public ref class WallUpdater sealed : Windows::UI::Xaml::Media::Imaging::XamlRenderingBackgroundTask
    {
	private:
		void SaveLockscreenBackground(StorageFile^ background);
		volatile bool CancelRequested;
	protected:
		void OnRun(Windows::ApplicationModel::Background::IBackgroundTaskInstance^ taskInstance) override;
		void OnCanceled(Windows::ApplicationModel::Background::IBackgroundTaskInstance^ taskInstance,
			Windows::ApplicationModel::Background::BackgroundTaskCancellationReason reason);
    public:
		WallUpdater();
    };
}
