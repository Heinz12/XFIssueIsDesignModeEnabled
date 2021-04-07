using System.ComponentModel;
using Xamarin.Forms;
using XFIssueIsDesignModeEnabled.ViewModels;

namespace XFIssueIsDesignModeEnabled.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}