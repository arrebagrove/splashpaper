using splashpaper.Controllers;
using splashpaper.Models;
using System;
using Windows.Storage;
using Windows.System.UserProfile;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace splashpaper.Views {
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            LoadData();
        }

        public async void LoadData() {
            Paper paper = await HomeController.GetRandom();
            var bitmap = new BitmapImage(new Uri(paper.thumbnail));
            Paper.Source = bitmap;

            //SetWallappaer(paper.urlRaw);
        }

        public async void SetWallappaer(string url) {
            StorageFile file = await HomeController.DownloadImagefromServer(url, "wall");

            bool success = false;
            if (UserProfilePersonalizationSettings.IsSupported()) {
                UserProfilePersonalizationSettings profileSettings = UserProfilePersonalizationSettings.Current;
                success = await profileSettings.TrySetWallpaperImageAsync(file);
            }
        }
    }
}
