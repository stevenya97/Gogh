using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Animation;
using WinRTXamlToolkit;
using WinRTXamlToolkit.AwaitableUI;
using WinRTXamlToolkit.Controls.Extensions;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Gogh_alpha
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        double from=0;
        double dblDelta_Scroll;
        StorageFile temp;
        string path = "ms-appx:///Assets/gogh.png";

        public MainPage()
        {
            //bool flag=ScrollViewerMain.ChangeView(null, null, 0.1f);
            MaximizeWindowOnLoad();

            this.InitializeComponent();
            InitialZoom();
            //txt.Text = Img.Source;
            void MaximizeWindowOnLoad()
            {
                var view = DisplayInformation.GetForCurrentView();

                // Get the screen resolution (APIs available from 14393 onward).
                var resolution = new Size(view.ScreenWidthInRawPixels, view.ScreenHeightInRawPixels);

                // Calculate the screen size in effective pixels. 
                // Note the height of the Windows Taskbar is ignored here since the app will only be given the maxium available size.
                var scale = view.ResolutionScale == ResolutionScale.Invalid ? 1 : view.RawPixelsPerViewPixel;
                var bounds = new Size(resolution.Width / scale, resolution.Height / scale);

                ApplicationView.PreferredLaunchViewSize = new Size(bounds.Width, bounds.Height);
                ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

                //lm1.Click += Lm1_Click;
                //tip.IsOpen = true;
                
            }

            async void ScreenCap()
            {
                var renderTargetBitmap = new RenderTargetBitmap();
                await renderTargetBitmap.RenderAsync(Window.Current.Content);
            }
            void InitialZoom()
            {
                //ScrollViewerMain.ChangeView(null, null, 1.0f);
                
                //ScrollViewerMain.ZoomToFactor(1.0f);
            }


        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ConnectedAnimation animation =ConnectedAnimationService.GetForCurrentView().GetAnimation("backAnimation");
            if (animation != null)
            {
                animation.TryStart(Img);
            }

            var args = e.Parameter as Windows.ApplicationModel.Activation.IActivatedEventArgs;
            if (args != null)
            {
                if (args.Kind == Windows.ApplicationModel.Activation.ActivationKind.File)
                {
                    var fileArgs = args as Windows.ApplicationModel.Activation.FileActivatedEventArgs;
                    string strFilePath = fileArgs.Files[0].Path;
                    var file = (StorageFile)fileArgs.Files[0];
                    await LoadImageFile(file);
                    txt.Text = strFilePath;
                    path = strFilePath;
                }
            }
        }
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("ForwardConnectedAnimation", Img);
            // You don't need to explicitly set the Configuration property because
            // the recommended Gravity configuration is default.
            // For custom animation, use:
            // animation.Configuration = new BasicConnectedAnimationConfiguration();
        }
        private async Task LoadImageFile(StorageFile file)
        {
            try {
                //var read = await FileIO.ReadTextAsync(file);
                //FileContents.Text = read;
                var read = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                BitmapImage imagebit = new BitmapImage();
                imagebit.SetSource(read);
                Img.Source = imagebit;
                //txt.Text = imagebit.UriSource.ToString();
                temp = file;
            }
            catch (Exception e)
            {
                //WriteMessageText(e.Message + "\n");
            }
            
        }
        void Img_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            // dim the image while panning
            this.Img.Opacity = 0.4;
        }

        void Img_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            this.Transform.TranslateX += e.Delta.Translation.X/ ScrollViewerMain.ZoomFactor;
            this.Transform.TranslateY += e.Delta.Translation.Y/ ScrollViewerMain.ZoomFactor;
            
        }

        void Img_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            // reset the Opacity
            this.Img.Opacity = 1;
        }

        void Image_PointerWheelChanged(object sender, PointerRoutedEventArgs e)

        {
            dblDelta_Scroll = -1 * e.GetCurrentPoint(Img).Properties.MouseWheelDelta;
            double temp = dblDelta_Scroll;
            dblDelta_Scroll = (dblDelta_Scroll > 0) ? 1.2 : -0.8;
            //e.GetCurrentPoint(sc)
            txt.Text = temp.ToString();
            double to = dblDelta_Scroll + ScrollViewerMain.ZoomFactor;
            WinRTXamlToolkit.Controls.Extensions.ScrollViewerExtensions.ZoomToFactorWithAnimationAsync(ScrollViewerMain, to);
            //ZoomToFactorWithAnimationAsync(to, 350);
            //double new_ScaleX = this.Transform.ScaleX / dblDelta_Scroll;
            //double new_ScaleY = this.Transform.ScaleY / dblDelta_Scroll;

            //this.Transform.ScaleX = new_ScaleX;
            //this.Transform.ScaleY = new_ScaleY;

            //x/y offset
            //    e.GetCurrentPoint(ScrollViewerMain).Properties.
            //    double dx = (currentMouseX - image.getLeft()) * (factor - 1),
            //dy = (currentMouseY - image.getTop()) * (factor - 1);
            //    // Compensate for displacement.
            //    image.setLeft(image.getLeft() - dx);
            //    image.setTop(image.getTop() - dy);
            //    this.Transform.TranslateX
        }

        //Crop
        private void Crop_Click(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
            //ScrollViewerMain.ZoomToFactor(1.0f);
            //ScrollViewerMain.ChangeView(2, null, null);

            string sr="";
            if (path != null)
            {
                sr = path;
                this.Frame.Navigate(typeof(ImageCropper), sr , new SuppressNavigationTransitionInfo());
            }
            else
            {
                sr = "Assets/gogh-tile.png";
                this.Frame.Navigate(typeof(ImageCropper), sr, new SuppressNavigationTransitionInfo());
            }
            
            System.Diagnostics.Debug.WriteLine("page 2");
        }
        //Rotate left
        void RotRightButton_Click(object sender, RoutedEventArgs e)
        {
            from += 90;
            AnimateRotation(from);
        }
        //Rotate right
        void RotLeftButton_Click(object sender, RoutedEventArgs e)
        {
            from -= 90;
            AnimateRotation(from);
            
        }
        //Open With
        async void OpenWith_ClickAsync(object sender, RoutedEventArgs e)
        {
            if(temp!=null)
            {
                var options = new Windows.System.LauncherOptions();
                options.DisplayApplicationPicker = true;
                await Windows.System.Launcher.LaunchFileAsync(temp, options);
            }
            else
            {
                ContentDialog noWifiDialog = new ContentDialog
                {
                    Title = "No file to open",
                    Content = "Load an image and try again.",
                    CloseButtonText = "Ok"
                };
                ContentDialogResult result = await noWifiDialog.ShowAsync();
                tip.IsOpen = true;
            }
            
        }
        private void AnimateRotation(double to, double miliseconds = 350)
        {
            var storyboard = new Storyboard();
            var doubleAnimation = new DoubleAnimation();
            doubleAnimation.Duration = TimeSpan.FromMilliseconds(miliseconds);
            doubleAnimation.EnableDependentAnimation = true;
            doubleAnimation.To = to;
            
            Storyboard.SetTargetProperty(doubleAnimation, "Rotation");
            Storyboard.SetTarget(doubleAnimation, Transform);
            CircleEase easing = new CircleEase();  // or whatever easing class you want
            easing.EasingMode = EasingMode.EaseInOut;
            doubleAnimation.EasingFunction = easing;
            storyboard.Children.Add(doubleAnimation);
            storyboard.Begin();
        }

        private void ActualSizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Transform.TranslateX = 0;
            this.Transform.TranslateY = 0;
            bool flag= ScrollViewerMain.ChangeView(null, null, 1);
            //ZoomToFactorWithAnimationAsync(0.1f, 250);
            
        }
        
         private void ZoomToFactorWithAnimationAsync(
            double factor,
            double miliseconds
            )
        {
            TimeSpan duration = TimeSpan.FromMilliseconds(miliseconds);
            EasingFunctionBase easingFunction = new CircleEase();
            easingFunction.EasingMode = EasingMode.EaseInOut;
            var sb = new Storyboard();
            var da = new DoubleAnimation();
            da.EnableDependentAnimation = true;
            da.From = ScrollViewerMain.ZoomFactor;
            da.To = factor;
            da.EasingFunction = easingFunction;
            da.Duration = duration;
            sb.Children.Add(da);
            Storyboard.SetTarget(sb, ScrollViewerMain);
            Storyboard.SetTargetProperty(da, "ZoomToFactor");
            ScrollViewerMain.ChangeView(0, 0, 1);
            //ScrollViewerMain.zoom
            sb.Begin();
        }

        private void View_Original_Click(object sender, RoutedEventArgs e)
        {
            this.Transform.TranslateX = 0;
            this.Transform.TranslateY = 0;
            from = 0;
            AnimateRotation(from);
            bool flag = ScrollViewerMain.ChangeView(null, null, 1);
            
        }

        private async void Edit_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = "Edit",
                Content = "Editing not avaliable on this build.",
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Settings), new SuppressNavigationTransitionInfo());
        }
    }
}
