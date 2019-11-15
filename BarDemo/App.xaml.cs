using System;
using BarDemo.Views;
using BarDemo.Models;
using BarDemo.Services;
using BarDemo.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BarDemo
{
    public partial class App : Application
    {

        public static double ScreenHeight;
        public static double ScreenWidth;


        // ToDo: Tie this to the OAuth2
        // If logged in through Facebook, Google, or other authentication provider,
        // then go directly to the TabPage. 
        public static bool IsUserLoggedIn { get; set; }

        public App()
        {
            InitializeComponent();


           MainPage = new NavigationPage(new TabPage());
           //MainPage = new TabPage();

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
