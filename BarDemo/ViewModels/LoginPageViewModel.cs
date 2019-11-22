using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using BarDemo.Models;
using Xamarin.Auth;
using BarDemo.Services;
using System.Threading.Tasks;

namespace BarDemo.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
        
    {
        //Binding command for FB Login button; subscribes/watches if button is clicked
        public Command OnFBButtonClickedCommand => new Command(OnFBButtonClicked);

        //Starts FB authentication process using FBAuthService
        private async void OnFBButtonClicked()
        {
            var FBAuth = new FBAuthService();

           await FBAuth.SignInAsync("421753182028932", new Uri("https://m.facebook.com/dialog/oauth/"), new Uri("https://bardemo.azurewebsites.net/.auth/login/facebook/callback"));

            FBAuth.FBDataRequest("name");

        }

        //public override Task Init()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
