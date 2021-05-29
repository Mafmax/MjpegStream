using MjpegStreamReciever.Model;
using MjpegStreamReciever.ViewModel.ViewArea;
using MjpegStreamReciever.ViewModel.ViewAreaProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MjpegStreamReciever.ViewModel
{
    public class MainViewModel
    {
        private readonly MainWindow window;

        public MainViewModel(MainWindow window, 
            IViewAreaProvider viewAreaProvider)
        {
            this.window = window;
            ViewAreaProvider = viewAreaProvider;

            ViewsOnWindow = new List<IViewArea>();
            CreateAddCommand();
            CreateRemoveCommand();
            CreatePauseCommand();
        }

        private void CreatePauseCommand()
        {

            PauseViewCommand = new RelayCommand(x => Pause(x as Button));
        }

        private void CreateRemoveCommand()
        {
            RemoveScreenCommand = new RelayCommand(x =>
              Remove(x as Button));
        }
        private void Remove(Button sender)
        {
            var viewArea = sender.DataContext as IViewArea;
            Pause(sender);
            int index = ViewsOnWindow.IndexOf(viewArea);
            ViewsOnWindow.RemoveAt(index);
            window.screensContainer.Children.RemoveAt(index);
        }
        private void Pause(Button sender)
        {
            var viewArea = sender.DataContext as SimpleViewArea;
            if(viewArea.Paused)
            {
                viewArea.Start();
                sender.Content = "Pause view";
            }
            else
            {
                viewArea.Pause();
                sender.Content = "Start view";
            }

        }
        private void CreateAddCommand()
        {
            AddScreenCommand = new RelayCommand(x => CreateNewScreen());
        }
        private void CreateNewScreen()
        {
            var viewArea = ViewAreaProvider.Provide();
            var content = viewArea.Content;
            content.DataContext = viewArea;
            window.screensContainer.Children.Add(content);
            window.scrollViewer.ScrollToEnd();
            ViewsOnWindow.Add(viewArea);
        }


        public IViewAreaProvider ViewAreaProvider { get; }
       
        public List<IViewArea> ViewsOnWindow { get; set; }
        public ICommand AddScreenCommand { get; set; }
        public ICommand RemoveScreenCommand { get; set; }
        public ICommand PauseViewCommand { get; set; }
    }
}
