using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Tasks.Models;
using Windows.ApplicationModel.Background;
using Windows.Storage;
using Newtonsoft.Json.Linq;
using Windows.System.UserProfile;
using Windows.UI.Xaml.Media.Imaging;
using System.Net.Http.Headers;

namespace Tasks {
    public sealed class WallUpdater : XamlRenderingBackgroundTask {
        BackgroundTaskDeferral _deferral;
        volatile bool _cancelRequested = false;
        private string unsplashURL = "https://unsplash.it/1500?random";

        private void OnCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason) {
            // Indicate that the background task is canceled.
            _cancelRequested = true;
        }

        protected override async void OnRun(IBackgroundTaskInstance taskInstance) {
            taskInstance.Canceled += OnCanceled;
            var deferral = taskInstance.GetDeferral();
            //string appBackgroundName = RetrieveLockscreenBackgroundName();
            //StorageFile wall = await DownloadImagefromServer(_lockscreenURL, newFilesName);
            //SaveLockscreenBackground(wall);
            //var prevName = RetrieveLockscreenBackgroundName();
            //var newName = GenerateAppBackgroundName(prevName);
            //var path = RetrieveAppBackgroundPath();
            //var dailyQuote = RetrieveDailyQuote();

            Paper paper = await GetRandom();

            StorageFile wall = await DownloadImagefromServer(paper.URLRaw, "wall24");
            //StorageFile wall = await ApplicationData.Current.LocalFolder.GetFileAsync(prevName); ;

            //StorageFile lockImage = await TakeScreenshot(wall.Path, newName, null);
            //await SetLockscreenAsync(wall);
            SetWallpaperAsync(wall);
            //SaveLockscreenBackgroundName(lockImage.Name);
            //SaveAppBackground(lockImage);
            deferral.Complete();
        }

        private async Task<Paper> GetRandom() {
            Paper paper = new Paper();

            HttpClient http = new HttpClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Client-ID", "16246a4d58baa698a0a720106aab4ecedfe241c72205586da6ab9393424894a8");
            HttpResponseMessage response = null;

            try {
                response = await http.GetAsync("https://api.unsplash.com/photos/random");
                response.EnsureSuccessStatusCode();
                string responseBodyAsText = await response.Content.ReadAsStringAsync();

                JObject json = JObject.Parse(responseBodyAsText);

                paper.Id = (string)json.GetValue("id");
                paper.Likes = (int)json.GetValue("likes");
                paper.URLRaw = (string)json["urls"]["raw"];
                paper.Thumbnail = (string)json["urls"]["thumb"];

                return paper;
            }
            catch (HttpRequestException hre) {
                return paper;
            }
        }

        private async Task<StorageFile> DownloadImagefromServer(string URI, string filename) {
            filename += ".png";
            var rootFolder = ApplicationData.Current.LocalFolder;
            var coverpic = await rootFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);

            try {
                HttpClient client = new HttpClient();
                byte[] buffer = await client.GetByteArrayAsync(URI); // Download file
                using (Stream stream = await coverpic.OpenStreamForWriteAsync())
                    stream.Write(buffer, 0, buffer.Length); // Save

                return coverpic;
            }
            catch {
                return null;
            }
        }

        // Pass in a relative path to a file inside the local appdata folder 
        private async Task<bool> SetLockscreenAsync(StorageFile file) {
            bool success = false;

            if (UserProfilePersonalizationSettings.IsSupported()) {
                UserProfilePersonalizationSettings profileSettings = UserProfilePersonalizationSettings.Current;
                success = await profileSettings.TrySetLockScreenImageAsync(file);
            }
            return success;
        }

        private async Task<bool> SetWallpaperAsync(StorageFile file) {
            bool success = false;

            if (UserProfilePersonalizationSettings.IsSupported()) {
                UserProfilePersonalizationSettings profileSettings = UserProfilePersonalizationSettings.Current;
                success = await profileSettings.TrySetWallpaperImageAsync(file);
            }
            return success;
        }
    }
}
