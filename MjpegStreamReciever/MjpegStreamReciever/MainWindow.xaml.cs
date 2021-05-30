using MjpegStreamReciever.Model;
using MjpegStreamReciever.Model.DataProviders;
using MjpegStreamReciever.ViewModel;
using MjpegStreamReciever.ViewModel.DataViewers;
using MjpegStreamReciever.ViewModel.Source;
using MjpegStreamReciever.ViewModel.ViewArea;
using MjpegStreamReciever.ViewModel.ViewAreaControlProviders;
using MjpegStreamReciever.ViewModel.ViewAreaParser;
using MjpegStreamReciever.ViewModel.ViewAreaProvider;
using MjpegStreamReciever.ViewModel.Viewport;
using Ninject;
using Ninject.Extensions.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MjpegStreamReciever
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string ResourcesPath => Environment.CurrentDirectory
                .Replace("\\bin\\Debug", "\\") + "Resources\\";
        public static int FPS = 60;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = GetViewModel();
        }

        private void BindFolderStream(StandardKernel kernel)
        {
            var streamImagesPath = ResourcesPath + "Images\\StreamImages";
            kernel.Bind<IViewAreaParser>().To<FolderPictureViewAreaParser>()
                .WithConstructorArgument("folderPath", streamImagesPath)
                .WithConstructorArgument("imageName", "viewport");
            kernel.Bind<DataProvider>().To<FolderPictureDataProvider>();
        }
        private void BindMjpegStream(StandardKernel kernel)
        {
            kernel.Bind<IViewAreaParser>().To<TextboxSourceViewAreaParser>()
                .WithConstructorArgument("textBoxName", "source")
                .WithConstructorArgument("imageName", "viewport");
            kernel.Bind<DataProvider>().To<MjpegDataProvider>();
        }
        private MainViewModel GetViewModel()
        {
            var container = new StandardKernel();
            container.Bind<IDataViewerFactory>().ToFactory();
            container.Bind<DataViewer>().To<SimplePictureDataViewer>()
                .WithConstructorArgument("fps", FPS)
                .WithConstructorArgument("errorPicturePath", ResourcesPath + "Images\\View error.png");
            container.Bind<IViewArea>().To<SimpleViewArea>();
            container.Bind<ViewAreaControlProvider>().To<TemplatedViewAreaControlProvider>()
                .WithConstructorArgument("window", this)
                .WithConstructorArgument("templateName", "movie");
            container.Bind<IViewAreaProvider>().To<SimpleViewAreaProvider>();
            container.Bind<MainViewModel>().To<MainViewModel>()
                .WithConstructorArgument("window", this);
            container.Bind<IDataProviderFactory>().ToFactory();
            //BindFolderStream(container);
            BindMjpegStream(container);

            return container.Get<MainViewModel>();
        }
    }
}
