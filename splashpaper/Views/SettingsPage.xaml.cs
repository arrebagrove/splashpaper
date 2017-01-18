using splashpaper.Controllers;
using System;
using Windows.ApplicationModel.Email;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace splashpaper.Views {
    public sealed partial class SettingsPage : Page {
        public SettingsPage() {
            InitializeComponent();
            LoadData();
        }

        private void LoadData() {
            WallSwitch.IsOn = SettingsController.IsBackgroundTaskActive();
            GetLastUpdatedTask();
            GetLastError();
        }

        private void GetLastUpdatedTask() {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey("wallstats")) {
                ApplicationDataCompositeValue stats = (ApplicationDataCompositeValue)localSettings.Values["wallstats"];

                if (stats["date"] == null) {
                    return;
                }

                LastUpdatedTask.Text = "Task last run on: " + stats["date"];
            }            
        }

        private void GetLastError() {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey("wallerror")) {
                ApplicationDataCompositeValue stats = (ApplicationDataCompositeValue)localSettings.Values["wallerror"];

                if (stats["date"] == null) {
                    return;
                }

                LastError.Text = "Last error on: " + stats["date"] + " " + " - due to: " + stats["error"];
            }
        }

        /// <summary>
        /// Add or remove background task when the toggle changes state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WallSwitch_Toggled(object sender, RoutedEventArgs e) {
            var toggle = (ToggleSwitch)sender;
            if (toggle.IsOn) {
                SettingsController.RegisterBackgroundTask(
                    SettingsController.GetTaskName(),
                    SettingsController.GetTaskEntryPoint());
            }
            else {
                SettingsController.UnregisterBackgroundTask(SettingsController.GetTaskName());
            }
        }

        private void FeedbackButton_Click(object sender, RoutedEventArgs e) {
            EmailMessage email = new EmailMessage();
            email.Subject = "[splashpaper] Feedback";
            email.Body = "send this email to metrodevapp@outlook.com";
            // TODO : add app infos
            EmailManager.ShowComposeNewEmailAsync(email);
        }

        private async void NoteButton_Click(object sender, RoutedEventArgs e) {
            string appID = "9wzdncrcwfqr";
            var op = await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store://review/?ProductId=" + appID));
        }
    }
}
