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


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Gogh_alpha
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        
        public MainPage()
        {
            //bool flag=ScrollViewerMain.ChangeView(null, null, 0.1f);
            MaximizeWindowOnLoad();

            this.InitializeComponent();
            InitialZoom();
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
                
            }

            async void ScreenCap()
            {
                var renderTargetBitmap = new RenderTargetBitmap();
                await renderTargetBitmap.RenderAsync(Window.Current.Content);
            }
            void InitialZoom()
            {
                //ScrollViewerMain.ChangeView(null, null, 1.0f);
                
                //ScrollViewerMain.ZoomToFactor(0.5f);
            }


        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var args = e.Parameter as Windows.ApplicationModel.Activation.IActivatedEventArgs;
            if (args != null)
            {
                if (args.Kind == Windows.ApplicationModel.Activation.ActivationKind.File)
                {
                    var fileArgs = args as Windows.ApplicationModel.Activation.FileActivatedEventArgs;
                    string strFilePath = fileArgs.Files[0].Path;
                    var file = (StorageFile)fileArgs.Files[0];
                    await LoadImageFile(file);
                }
            }
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
            }
            catch (Exception e)
            {
                //WriteMessageText(e.Message + "\n");
            }
            
        }
        void Img_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            // dim the image while panning
            //this.Img.Opacity = 0.4;
        }

        void Img_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            this.Transform.TranslateX += e.Delta.Translation.X/ ScrollViewerMain.ZoomFactor;
            this.Transform.TranslateY += e.Delta.Translation.Y/ ScrollViewerMain.ZoomFactor;
            
        }

        void Img_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            // reset the Opacity
            //this.Img.Opacity = 1;
        }

        private void Image_PointerWheelChanged(object sender, PointerRoutedEventArgs e)

        {
            double dblDelta_Scroll = -1 * e.GetCurrentPoint(Img).Properties.MouseWheelDelta;
            dblDelta_Scroll = (dblDelta_Scroll > 0) ? 1.2 : 0.8;

            double new_ScaleX = this.Transform.ScaleX / dblDelta_Scroll;
            double new_ScaleY = this.Transform.ScaleY / dblDelta_Scroll;

            this.Transform.ScaleX = new_ScaleX;
            this.Transform.ScaleY = new_ScaleY;

            //x/y offset
        //    e.GetCurrentPoint(ScrollViewerMain).Properties.
        //    double dx = (currentMouseX - image.getLeft()) * (factor - 1),
        //dy = (currentMouseY - image.getTop()) * (factor - 1);
        //    // Compensate for displacement.
        //    image.setLeft(image.getLeft() - dx);
        //    image.setTop(image.getTop() - dy);
        //    this.Transform.TranslateX
        }

            private void Lm1_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
            //ScrollViewerMain.ZoomToFactor(1.0f);
        }
    }
}
