#include "pch.h"
#include "WallUpdater.h"

using namespace OptimizedTasks;
using namespace Platform;
using namespace concurrency;
using namespace Windows::ApplicationModel::Background;
using namespace Windows::System::UserProfile;

WallUpdater::WallUpdater()
{
}

void WallUpdater::OnRun(IBackgroundTaskInstance^ taskInstance) {
	Agile<BackgroundTaskDeferral^> deferral = Agile<BackgroundTaskDeferral^>(taskInstance->GetDeferral());
	taskInstance->Canceled += ref new BackgroundTaskCanceledEventHandler(this, &WallUpdater::OnCanceled);

	//if (UserProfilePersonalizationSettings::Current->IsSupported) {
	//	String^ prevName = RetrieveLockscreenBackgroundName();
	//	String^ newName = GenerateAppBackgroundName(prevName);

	//	create_task(StorageFile::GetFileFromApplicationUriAsync(ref new Uri("ms-appx:///Assets/lockScreen.xml"))).then([](StorageFile^ xamlFile) {
	//		return FileIO::ReadTextAsync(xamlFile);

	//	}).then([this, newName](String^ content) {

	//		String^ uri = "https://unsplash.it/720/1080?random";
	//		Uri^ _uri = ref new Uri(uri);
	//		ApplicationData^ current = ApplicationData::Current;

	//		return create_task(StorageFile::CreateStreamedFileFromUriAsync(newName, _uri, RandomAccessStreamReference::CreateFromUri(_uri)))
	//			.then([current, newName](StorageFile^ f) { // Download image
	//			return f->CopyAsync(current->LocalFolder, newName, NameCollisionOption::ReplaceExisting);

	//		}).then([](StorageFile^ f2) {
	//			return Windows::System::UserProfile::UserProfilePersonalizationSettings::Current->TrySetLockScreenImageAsync(f2);
	//		});
	//			
	//	}).then([deferral](bool ok) {
	//		deferral->Complete();
	//	});

	//}
	//else {
	//	// todo ...
	//	deferral->Complete();
	//}
}

//void WallUpdater::SaveLockscreenBackground(StorageFile^ background) {
//	ApplicationData^ current = ApplicationData::Current;
//	ApplicationDataContainer^ localSettings = current->LocalSettings;
//	localSettings->Values->Insert("wall.png", background->Name);
//	localSettings->Values->Insert(_lockscreenBackgroundPath, background->Path);
//}

void WallUpdater::OnCanceled(
	Windows::ApplicationModel::Background::IBackgroundTaskInstance^ taskInstance,
	BackgroundTaskCancellationReason reason) {
	// TODO: Add code to notify the background task that it is cancelled.
	CancelRequested = true;
}