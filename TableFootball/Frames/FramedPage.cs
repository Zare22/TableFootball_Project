using System.Windows.Controls;
using TableFootball.ViewModels;

namespace TableFootball.Frames
{
    public class FramedPage : Page
    {
        private readonly IViewModel _viewModel;

        public FramedPage(IViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public IViewModel ViewModel => _viewModel;

        public Frame Frame { get; set; }
    }
}
