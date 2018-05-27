using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinWebservice.View;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamarinWebservice
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ListPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
