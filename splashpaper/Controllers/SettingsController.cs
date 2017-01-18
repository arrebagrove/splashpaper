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

        public static async void RegisterBackgroundTask(string taskName, string entryPoint) {
            foreach (var task in BackgroundTaskRegistration.AllTasks) {
                if (task.Value.Name == taskName) {
                    return;
                }
            }

            BackgroundExecutionManager.RemoveAccess();
            BackgroundAccessStatus status = await BackgroundExecutionManager.RequestAccessAsync();
            if (status == BackgroundAccessStatus.Denied) {
                return;
            }

            SystemCondition internetCondition = new SystemCondition(SystemConditionType.InternetAvailable);

            var builder = new BackgroundTaskBuilder();

            builder.Name = taskName;
            builder.TaskEntryPoint = entryPoint;
            var timeTrigger = new TimeTrigger(16, false);
            builder.SetTrigger(timeTrigger);
            //builder.SetTrigger(new SystemTrigger(SystemTriggerType.PowerStateChange, false));
            builder.AddCondition(internetCondition);
            BackgroundTaskRegistration taskRegistered = builder.Register();
        }

        public static async void RegisterSingleProcessTask() {
            string taskName = "SingleTask";
            foreach (var registeredTask in BackgroundTaskRegistration.AllTasks) {
                if (registeredTask.Value.Name == taskName) {
                    return;
                }
            }

            BackgroundAccessStatus status = await BackgroundExecutionManager.RequestAccessAsync();
            if (status == BackgroundAccessStatus.Denied) {
                return;
            }

            var builder = new BackgroundTaskBuilder();
            builder.Name = "SingleTaskTrigger";
            builder.SetTrigger(new TimeTrigger(15, true));

            // use builder.TaskEntryPoint if you want to not use the default OnBackgroundActivated
            // we’ll register it and now will start work based on the trigger, here we used a Time Trigger
            BackgroundTaskRegistration task = builder.Register();
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
