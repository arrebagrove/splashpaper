using Newtonsoft.Json.Linq;
using splashpaper.Models;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace splashpaper.Controllers {
    public static class HomeController {
        public static void GetLast() {
            
        }

        public static async Task<Paper> GetRandom() {
            Paper paper = new Paper();

            HttpClient http = new HttpClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Client-ID", "16246a4d58baa698a0a720106aab4ecedfe241c72205586da6ab9393424894a8");
            HttpResponseMessage response = null;

            try {
                response = await http.GetAsync("https://api.unsplash.com/photos/random");
                response.EnsureSuccessStatusCode();
                string responseBodyAsText = await response.Content.ReadAsStringAsync();

                JObject json = JObject.Parse(responseBodyAsText);

                paper.id = (string)json.GetValue("id");
                paper.likes = (int)json.GetValue("likes");
                paper.urlRaw = (string)json["urls"]["raw"];
                paper.thumbnail = (string)json["urls"]["thumb"];

                return paper;
            }
            catch (HttpRequestException hre) {
                return paper;
            }
        }

        public static async Task<StorageFile> DownloadImagefromServer(string URI, string filename) {
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

        public static async void SavePicture(BitmapImage bitmap) {
            try {
                await DataSerializer<BitmapImage>.SaveObjectsAsync(bitmap, "wall.png");
            }
            catch (Exception ex) {
                throw;
            }
        }

        public static async Task<BitmapImage> GetPicture() {
            try {
                BitmapImage image = await DataSerializer<BitmapImage>.RestoreObjectsAsync("wall.png");
                return image;
            }
            catch (Exception ex) {
                return null;
            }
        }
    }
}
