using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.ApplicationModel.Core;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Gogh_alpha
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ImageCropper : Page
    {
        public ImageCropper()
        {
            this.InitializeComponent();
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            //this.Frame.Navigate(typeof(MainPage), new SuppressNavigationTransitionInfo());
            this.Frame.GoBack();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter!= null)
            {
                //imageCropper.Source = e.Parameter.;
                txt.Text = (String)e.Parameter.ToString();
                
                if(txt.Text!= "ms-appx:///Assets/gogh.png")
                {
                    var file = await StorageFile.GetFileFromPathAsync(txt.Text);
                    var read = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                    BitmapImage imagebit = new BitmapImage();
                    imagebit.SetSource(read);
                    transition.Source = imagebit;
                    await imageCropper.LoadImageFromFile(file);
                }
                else
                {
                    var file=await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/gogh.png"));
                    await imageCropper.LoadImageFromFile(file);
                }
                    
                    
                //await imageCropper.LoadImageFromFile(file);
                //LoadImage((ImageSource)e.Parameter);
                //imageCropper.Source = "Assets/gogh-tile.png";
                //imageCropper.LoadImageFromFile("Assets/gogh-tile.png");
                System.Diagnostics.Debug.WriteLine("test");
                var anim = ConnectedAnimationService.GetForCurrentView().GetAnimation("ForwardConnectedAnimation");
                if (anim != null)
                {
                    anim.TryStart(transition);
                }

            }
            else
            {
                //greeting.Text = "Hi!";
                System.Diagnostics.Debug.WriteLine("fail");
                
            }
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                ConnectedAnimation animation =
                    ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("backAnimation", transition);

                // Use the recommended configuration for back animation.
                animation.Configuration = new DirectConnectedAnimationConfiguration();
            }
            else
            {
                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("backAnimation", transition);
            }
        }
    }
}
