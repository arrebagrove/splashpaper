using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace splashpaper.Controllers {
    public static class SettingsController {
        private static string _taskName = "WallUpdaterTask";
        private static string _taskEntryPoint = "Tasks.WallUpdater";

        public static string GetTaskName() {
            return _taskName;
        }

        public static string GetTaskEntryPoint() {
            return _taskEntryPoint;
        }

        public static bool IsBackgroundTaskActive() {
            foreach (var task in BackgroundTaskRegistration.AllTasks) {
                if (task.Value.Name == GetTaskName()) {
                    return true;
                }
            }
            return false;
        }

        public static void RegisterBackgroundTask(string taskName, string entryPoint) {
            foreach (var task in BackgroundTaskRegistration.AllTasks) {
                if (task.Value.Name == taskName) {
                    return;
                }
            }

            BackgroundExecutionManager.RequestAccessAsync();

            var builder = new BackgroundTaskBuilder();

            builder.Name = taskName;
            builder.TaskEntryPoint = entryPoint;
            builder.SetTrigger(new TimeTrigger(15, false));
            BackgroundTaskRegistration taskRegistered = builder.Register();
        }

        public static void UnregisterBackgroundTask(string taskName) {
            foreach (var task in BackgroundTaskRegistration.AllTasks) {
                if (task.Value.Name == taskName) {
                    BackgroundExecutionManager.RemoveAccess();
                    task.Value.Unregister(false);
                    break;
                }
            }
        }
    }
}
