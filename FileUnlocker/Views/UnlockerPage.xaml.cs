using FileUnlocker.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace FileUnlocker.Views
{
    public sealed partial class UnlockerPage : Page
    {
        public UnlockerViewModel ViewModel
        {
            get;
        }

        public UnlockerPage()
        {
            ViewModel = App.GetService<UnlockerViewModel>();
            InitializeComponent();
            
        }
    }
}
