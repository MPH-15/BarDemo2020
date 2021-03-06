﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Xamarin.Auth;
using Xamarin.Essentials;
using Newtonsoft.Json;
using System.Net.Http;
using System.Json;
using Xamarin.Forms;
using BarDemo.Views;
using BarDemo.Services;
using BarDemo.Models;
using System.Threading.Tasks;
using Xamarin.Auth;



namespace BarDemo.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public bool FBAuth;
        Account account;


        Command _fbCommand;
        public Command FBCommand
        {
            get
            {
                return _fbCommand ?? (_fbCommand = new Command(async () => await ExecuteFBCommand()));
            }
        }


        public LoginViewModel(INavService navService) : base(navService)
        {

        }

        public override async Task Init()
        {

        }


      

        public void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnFBAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            Debug.WriteLine("Authentication error: " + e.Message);
        }




        public void FBLoginButtonClicked()
        {

            string clientId = null;
            string redirectUri = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    clientId = Constants.FBiOSClientId;
                    redirectUri = Constants.FBiOSRedirectUrl;
                    break;

                case Device.Android:
                    clientId = Constants.FBAndroidClientId;
                    redirectUri = Constants.FBAndroidRedirectUrl;
                    break;
            }


            var FBauthenticator = new OAuth2Authenticator(
             clientId: clientId,
             scope: "email, user_gender, user_birthday,user_age_range",
             authorizeUrl: new Uri(Constants.FBAuthorizeUrl),
             redirectUrl: new Uri(Constants.FBiOSRedirectUrl),
             isUsingNativeUI: false
             );

            FBauthenticator.Completed += OnFBAuthCompleted;
            FBauthenticator.Error += OnAuthError;

            AuthenticationState.Authenticator = FBauthenticator;



            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(FBauthenticator);

        }



        public async void OnFBAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            try
            {
                await SecureStorage.SetAsync("oauth_token", e.Account.ToString());
            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
            }

            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnFBAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            FBUser user = null;
            if (e.IsAuthenticated)
            {
                FBAuth = true;
                Debug.WriteLine("Facebook Authenticated! ");

                var request = new OAuth2Request(
                    "GET",
                    new Uri("https://graph.facebook.com/me?fields=name,gender,birthday,age_range"),
                    null,
                    e.Account);

                var response = await request.GetResponseAsync();
                if (response != null)
                {
                    string userJson = await response.GetResponseTextAsync();
                    user = JsonConvert.DeserializeObject<FBUser>(userJson);

                    Debug.WriteLine("User Name: " + user.Name);
                    Debug.WriteLine("User Gender: " + user.Gender);
                    Debug.WriteLine("User Birthday: " + user.DoB);
                    Debug.WriteLine("Age Range: " + user.age_range.min);
                    Debug.WriteLine("Age Range: " + user.age_range.max);

                }


                if (account != null)
                {
                    //store.Delete(account, Constants.AppName);
                    Debug.WriteLine("Account isn't null");
                }

                await ExecuteSearchCommand(user);

            }
        }



        async Task ExecuteFBCommand()
        {
            FBLoginButtonClicked();
        }


        async Task ExecuteSearchCommand(FBUser fb_user)
        {
            await NavService.NavigateTo<TabViewModel, FBUser>(fb_user);
            await NavService.RemoveLastView();
        }



    }
}