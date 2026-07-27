using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Gogh
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public string Version => App.CurrentApp.GetVersionString();
        public SettingsPage()
        {
            this.InitializeComponent();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
        void EnableOneDrive_Toggled(object sender, RoutedEventArgs e)
        {
            if (DataContext == null)
            {
                return;
            }

            if (e.OriginalSource is ToggleSwitch toggleSwitch)
            {
                //Settings.Instance.EnableOneDrive = toggleSwitch.IsOn;
            }
        }
        void EnableiCloud_Toggled(object sender, RoutedEventArgs e)
        {
            if (DataContext == null)
            {
                return;
            }

            if (e.OriginalSource is ToggleSwitch toggleSwitch)
            {
                //Settings.Instance.EnableOneDrive = toggleSwitch.IsOn;
            }
        }

        private static string GetAppVersionDescription()
        {
            var assemblyInformationalVersion = (AssemblyInformationalVersionAttribute)Assembly.GetExecutingAssembly().GetCustomAttribute(typeof(AssemblyInformationalVersionAttribute));
            return string.Format(App.CurrentApp.GetVersionString(), assemblyInformationalVersion.InformationalVersion);
        }
        public async void OpenSystemPermissions(object sender, RoutedEventArgs e)
        {
            bool result = await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-broadfilesystemaccess"));
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
