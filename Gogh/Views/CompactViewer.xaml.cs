using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Gogh
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CompactViewer : Page
    {
        public Size SavedSize { get; set; } = new Size
        {
            Width = 400,
            Height = 225
        };
        public CompactViewer()
        {
            this.InitializeComponent();
            //CompactImg.Height= ((Frame)Window.Current.Content).ActualHeight;
            System.Diagnostics.Debug.WriteLine(CompactImg.Height);
        }

        private async void Back_Click(object sender, RoutedEventArgs e)
        {
            await ApplicationView.GetForCurrentView().TryEnterViewModeAsync(ApplicationViewMode.Default);
            Frame.Navigate(typeof(MainPage), null,new SuppressNavigationTransitionInfo());
        }
        private void Window_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            if (ApplicationView.GetForCurrentView().ViewMode == ApplicationViewMode.CompactOverlay)
            {
                SavedSize = CalculateNewSize(e.Size);
                UpdateImgBounds(e.Size.Width, e.Size.Height);
                System.Diagnostics.Debug.WriteLine(e.Size);
            }
            System.Diagnostics.Debug.WriteLine(e.Size);
        }
        Size CalculateNewSize(Size s)
        {
            s.Height -= 14;
            return s;
        }
        void UpdateImgBounds(double w, double h)
        {
            CompactImg.Width = w;
            CompactImg.Height = h-14;
            //System.Diagnostics.Debug.WriteLine(CompactImg.Height);
        }
    }
}
