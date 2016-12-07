using Scheduler.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Scheduler.ViewModel
{
    public class MainWindowViewModel : ObservableObject
    {
        private ICommand _changePageCommand;
        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;

        public MainWindowViewModel()
        {
            PageViewModels.Add(new HomePageViewModel());
            PageViewModels.Add(new ScheduleViewModel());
            PageViewModels.Add(new NewScheduleViewModel());
            CurrentPageViewModel = PageViewModels[0];


        }

        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IPageViewModel>();
                return _pageViewModels;
            }
        }

        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new RelayCommand
                        (
                            p => ChangeViewModel((IPageViewModel)p),
                            p => p is IPageViewModel
                        );

                }
                return _changePageCommand;
            }
        }

        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }

            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    OnPropertyChanged("CurrentPageViewModel");
                }
            }

        }

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels.FirstOrDefault(vm => vm == viewModel);
        }
    }
}
