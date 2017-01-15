using splashpaper.Views;
using splashpaper.Presentation;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace splashpaper {
    public sealed partial class Shell : UserControl
    {
        public Shell()
        {
            this.InitializeComponent();

            var vm = new ShellViewModel();

            vm.MenuItems.Add(new MenuItem { 
                Icon = "",
                SymbolAsChar = '\uE706',
                Label = "News",
                PageType = typeof(HomePage)
            });
            vm.MenuItems.Add(new MenuItem {
                Icon = "",
                SymbolAsChar = '\uE00B',
                Label = "Favorites",
                PageType = typeof(FavoritesPage)
            });
            vm.MenuItems.Add(new MenuItem {
                Icon = "",
                SymbolAsChar = '\uE00B',
                Label = "Random",
                PageType = typeof(RandomPage)
            });
            vm.MenuItems.Add(new MenuItem {
                SymbolAsChar = '\uE115',
                Label = "Settings",
                PageType = typeof(SettingsPage)
            });

            // select the first menu item
            vm.SelectedMenuItem = vm.MenuItems.First();

            this.ViewModel = vm;

            // add entry animations
            var transitions = new TransitionCollection { };
            var transition = new NavigationThemeTransition { };
            transitions.Add(transition);
            this.Frame.ContentTransitions = transitions;
        }

        public ShellViewModel ViewModel { get; private set; }

        public Frame RootFrame
        {
            get
            {
                return this.Frame;
            }
        }
    }
}
