using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFIssueIsDesignModeEnabled.Services;
using XFIssueIsDesignModeEnabled.Views;

namespace XFIssueIsDesignModeEnabled
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            if (DesignMode.IsDesignModeEnabled) 
                return;


            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
