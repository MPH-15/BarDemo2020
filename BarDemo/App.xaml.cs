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

            var navService = DependencyService.Get<INavService>() as NavService;

           // ToDo: Don't require that someone logs in everytime. Save Is 
           // User Logged in somewhere. 
            if (!IsUserLoggedIn)
            {
                MainPage = new NavigationPage(new LoginPage());
                //MainPage = new NavigationPage(new BarListPage());
            }
            else
            {
                MainPage = new NavigationPage(new TabPage());
                //MainPage = new TabPage();
            }

            navService.XamarinFormsNav = MainPage.Navigation;

            navService.RegisterViewMapping(typeof(SearchViewModel), typeof(TabPage));
            navService.RegisterViewMapping(typeof(LoginViewModel), typeof(LoginPage));
            navService.RegisterViewMapping(typeof(ProfileViewModel), typeof(ProfilePage));
            navService.RegisterViewMapping(typeof(BarListViewModel), typeof(BarListPage));
            navService.RegisterViewMapping(typeof(MapViewModel), typeof(MapPage));

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
